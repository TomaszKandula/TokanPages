import * as React from "react";
import { Box, Grid } from "@material-ui/core";
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

        const renderContext = {
            canvasContext: context,
            viewport: viewport
        };

        const renderTask = page.render(renderContext);
        renderTask.promise.then(() => {
            console.debug("Page rendered...");
        });

    }, [pdfDocument, currentPage]);

    const getDocument = React.useCallback(async () => {
        const doc = await pdfjsLib.getDocument(url).promise;
        setNumPages(doc._pdfInfo.numPages);
        setPdfDocument(doc);
        setLoading(false);
        setCurrentPage(1);
    }, []);

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
        <Grid container justifyContent="center" direction="column">
            {isLoading 
            ? <ProgressBar /> 
            : <Box className={classes.header}>{currentPage} / {numPages}</Box>}
            <canvas id={handleId}></canvas>
        </Grid>
    );
}
