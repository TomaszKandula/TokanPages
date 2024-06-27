import * as React from "react";
import { Box, Grid } from "@material-ui/core";
import NavigateBeforeIcon from "@material-ui/icons/NavigateBefore";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import { GET_DOCUMENTS_URL } from "../../../Api/Request";
import { PDF_WORKER_URL } from "../../../Shared/constants";
import { ProgressBar } from "../../../Shared/Components";
import { PdfViewerStyle } from "./pdfViewerStyle";

interface PdfViewerViewProps {
    pdfFile: string;
    scale?: number;
}

export const PdfViewerView = (props: PdfViewerViewProps): JSX.Element => {
    //@ts-expect-error
    let { pdfjsLib } = globalThis;
    pdfjsLib.GlobalWorkerOptions.workerSrc = PDF_WORKER_URL;

    const handleId = "pdf-canvas";
    const scale = props.scale ?? 1.5;
    const url = GET_DOCUMENTS_URL + "/" + props.pdfFile;
    const classes = PdfViewerStyle();

    const [isLoading, setLoading] = React.useState(true);
    const [pdfDocument, setPdfDocument] = React.useState<any>(null);
    const [numPages, setNumPages] = React.useState(0);
    const [currentPage, setCurrentPage] = React.useState(0);

    const renderPage = React.useCallback(async (numPage: number) => {
        const page = await pdfDocument.getPage(numPage);

        let canvas = document.querySelector(`#${handleId}`) as HTMLCanvasElement | null;
        if (canvas === null) {
            return;
        }

        const viewport = page.getViewport({ scale: scale });
        const context = canvas.getContext("2d");
        canvas.height = viewport.height;
        canvas.width = viewport.width;

        const renderContext = { canvasContext: context, viewport: viewport };
        const renderTask = page.render(renderContext);
        renderTask.promise.then(() => {
            renderTask.cancel();
        });

    }, [pdfDocument, currentPage]);

    const getDocument = React.useCallback(async () => {
        const doc = await pdfjsLib.getDocument(url).promise;
        setNumPages(doc._pdfInfo.numPages);
        setPdfDocument(doc);
        setLoading(false);
        setCurrentPage(1);
    }, []);

    const nextPage = React.useCallback(() => {
        if (numPages === currentPage) {
            return;
        }

        const next = currentPage + 1;
        setCurrentPage(next);
        renderPage(next);
    }, [numPages, currentPage]);

    const previousPage = React.useCallback(() => {
        if (currentPage === 1) {
            return;
        }

        const previous = currentPage - 1;
        setCurrentPage(previous);
        renderPage(previous);
    }, [numPages, currentPage]);

    React.useEffect(() => {
        if (pdfDocument !== null && currentPage > 0) {
            renderPage(currentPage);
        }
    }, [pdfDocument, currentPage]);

    React.useEffect(() => {
        if (isLoading) {
            getDocument();
        }
    }, [isLoading]);

    return (
        <section className={classes.section}>
            <Grid container justifyContent="center" direction="column">
                {isLoading ? <ProgressBar /> : <></>}
                <Box mt={2} pt={2} pb={2} className={classes.header}>
                    <div className={classes.header_pages}>
                        {currentPage} / {numPages}
                    </div>
                    <div>
                        <NavigateBeforeIcon className={classes.header_buttons} onClick={previousPage} />
                        <NavigateNextIcon className={classes.header_buttons} onClick={nextPage} />
                    </div>
                </Box>
                <Box className={classes.canvasWrapper}>
                    <canvas id={handleId} className={classes.canvas}></canvas>
                </Box>
            </Grid>
        </section>
    );
}
