import * as React from "react";
import { Box, Container, Grid } from "@material-ui/core";
import NavigateBeforeIcon from "@material-ui/icons/NavigateBefore";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import CheckIcon from "@material-ui/icons/Check";
import ReportProblemIcon from "@material-ui/icons/ReportProblem";
import { BackArrow, ProgressBar } from "../../../Shared/Components";
import { PdfViewerStyle } from "./pdfViewerStyle";

interface PdfViewerViewProps {
    isLoading: boolean;
    hasError: boolean;
    currentPage: number;
    numPages: number;
    pdfDocument: any;
    scale?: number;
    background?: React.CSSProperties;
    previousPage: () => void;
    nextPage: () => void;
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

const RenderIcon = (props: RenderIconOrErrorProps) => {
    return props.hasError ? <ReportProblemIcon /> : <CheckIcon />;
};

const RenderIconOrLoading = (props: RenderIconOrErrorProps): JSX.Element => {
    return props.isLoading && !props.hasError ? <ProgressBar size={20} /> : <RenderIcon {...props} />;
};

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
    const classes = PdfViewerStyle();
    return (
        <section className={classes.section} style={props.background}>
            <Container className={classes.container}>
                <Box pt={4} pb={6}>
                    <Box pt={0} pb={6}>
                        <BackArrow className={classes.back_arrow} />
                    </Box>
                    <Grid container justifyContent="center" direction="column">
                        <Box pt={2} pb={2} className={classes.header}>
                            <RenderIconOrLoading isLoading={props.isLoading} hasError={props.hasError} />
                            <div className={classes.header_pages}>
                                {props.currentPage} / {props.numPages}
                            </div>
                            <div>
                                <NavigateBeforeIcon className={classes.header_buttons} onClick={props.previousPage} />
                                <NavigateNextIcon className={classes.header_buttons} onClick={props.nextPage} />
                            </div>
                        </Box>
                        <Box className={classes.canvasWrapper}>
                            <PdfCanvas
                                pdfDocument={props.pdfDocument}
                                pageNumber={props.currentPage}
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
