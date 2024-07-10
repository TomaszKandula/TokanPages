import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Box, Container, Grid } from "@material-ui/core";
import NavigateBeforeIcon from "@material-ui/icons/NavigateBefore";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import CheckIcon from "@material-ui/icons/Check";
import ReportProblemIcon from "@material-ui/icons/ReportProblem";
import { ApplicationState } from "../../../Store/Configuration";
import { GET_DOCUMENTS_URL } from "../../../Api/Request";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { PDF_WORKER_URL } from "../../../Shared/constants";
import { BackArrow, ProgressBar } from "../../../Shared/Components";
import { PdfViewerStyle } from "./pdfViewerStyle";

interface PdfViewerViewProps {
    pdfFile: string;
    scale?: number;
    background?: React.CSSProperties;
}

interface PdfCanvasProps {
    pdfDocument: any;
    pageNumber: number;
    scale: number;
    htmlAttributes: React.HTMLAttributes<HTMLCanvasElement>;
}

interface RenderIconOrErrorProps {
    isLoading: boolean;
    hasError: boolean;
}

const RenderIconOrError = (props: RenderIconOrErrorProps): JSX.Element => {
    const RenderIcon = () => props.hasError 
    ? <ReportProblemIcon />
    : <CheckIcon />;

    return props.isLoading && !props.hasError ? <ProgressBar size={20} /> : <RenderIcon />;
}

const PdfCanvas = (props: PdfCanvasProps): JSX.Element => {
    const canvasRef = React.useRef<HTMLCanvasElement | null>(null);

    const renderPage = React.useCallback(
        async (numPage: number, canvas: HTMLCanvasElement, context: CanvasRenderingContext2D) => {
            const page = await props.pdfDocument.getPage(numPage);
            const viewport = page.getViewport({ scale: props.scale });

            canvas.height = viewport.height;
            canvas.width = viewport.width;

            const renderContext = { canvasContext: context, viewport: viewport };
            const renderTask = page.render(renderContext);

            renderTask.promise.then(() => {
                renderTask.cancel();
            });
        },
        [props.pdfDocument]
    );

    React.useEffect(() => {
        const canvas = canvasRef.current;
        if (canvas === null) {
            return;
        }

        const context = canvas.getContext("2d");
        if (context === null) {
            return;
        }

        if (props.pdfDocument !== null && props.pageNumber > 0) {
            renderPage(props.pageNumber, canvas, context);
        }
    }, [props.pdfDocument, props.pageNumber]);

    return <canvas ref={canvasRef} {...props.htmlAttributes} />;
};

export const PdfViewerView = (props: PdfViewerViewProps): JSX.Element => {
    //@ts-expect-error
    let { pdfjsLib } = globalThis;
    pdfjsLib.GlobalWorkerOptions.workerSrc = PDF_WORKER_URL;
    const url = `${GET_DOCUMENTS_URL}/${props.pdfFile}`;

    const dispatch = useDispatch();
    const classes = PdfViewerStyle();

    const template = useSelector((state: ApplicationState) => state.contentTemplates?.content.templates.application);
    const hasTemplates =
        template.nullError !== "" &&
        template.unexpectedError !== "" &&
        template.unexpectedStatus !== "" &&
        template.validationError !== "";

    const [isLoading, setLoading] = React.useState(true);
    const [hasError, setError] = React.useState(false);
    const [pdfDocument, setPdfDocument] = React.useState<any>(null);
    const [numPages, setNumPages] = React.useState(0);
    const [currentPage, setCurrentPage] = React.useState(0);

    const getDocument = React.useCallback(async () => {
        let doc;
        try {
            doc = await pdfjsLib.getDocument(url).promise;
        } catch (error: any) {
            const statusText = template.unexpectedStatus.replace("{STATUS_CODE}", error.status.toString());
            setError(true);
            RaiseError({
                dispatch: dispatch,
                errorObject: statusText,
                content: template,
            });

            return;
        }

        setNumPages(doc._pdfInfo.numPages);
        setPdfDocument(doc);
        setLoading(false);
        setCurrentPage(1);
    }, [template]);

    const nextPage = React.useCallback(() => {
        if (numPages === currentPage) {
            return;
        }

        const next = currentPage + 1;
        setCurrentPage(next);
    }, [numPages, currentPage]);

    const previousPage = React.useCallback(() => {
        if (currentPage === 1) {
            return;
        }

        const previous = currentPage - 1;
        setCurrentPage(previous);
    }, [numPages, currentPage]);

    React.useEffect(() => {
        if (isLoading && hasTemplates) {
            getDocument();
        }
    }, [isLoading, hasTemplates]);

    return (
        <section className={classes.section} style={props.background}>
            <Container className={classes.container}>
                <Box pt={4} pb={6}>
                    <Box pt={0} pb={6}>
                        <BackArrow className={classes.back_arrow} />
                    </Box>
                    <Grid container justifyContent="center" direction="column">
                        <Box pt={2} pb={2} className={classes.header}>
                            <RenderIconOrError isLoading={isLoading} hasError={hasError} />
                            <div className={classes.header_pages}>
                                {currentPage} / {numPages}
                            </div>
                            <div>
                                <NavigateBeforeIcon className={classes.header_buttons} onClick={previousPage} />
                                <NavigateNextIcon className={classes.header_buttons} onClick={nextPage} />
                            </div>
                        </Box>
                        <Box className={classes.canvasWrapper}>
                            <PdfCanvas
                                pdfDocument={pdfDocument}
                                pageNumber={currentPage}
                                scale={props.scale ?? 1.5}
                                htmlAttributes={{ className: classes.canvas }}
                            />
                        </Box>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
};
