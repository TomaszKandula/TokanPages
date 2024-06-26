import * as React from "react";
import { Box } from "@material-ui/core";
import { GET_DOCUMENTS_URL } from "../../../Api/Request";
import { PDF_WORKER_URL } from "../../../Shared/constants";

interface PdfViewerViewProps {
    pdfFile: string;
    scale?: number;
}

export const PdfViewerView = (props: PdfViewerViewProps): JSX.Element => {
    //@ts-expect-error
    let { pdfjsLib } = globalThis;
    pdfjsLib.GlobalWorkerOptions.workerSrc = PDF_WORKER_URL;

    const handleId = "pdf-canvas";
    const scale = props.scale ?? 1.2;
    const url = GET_DOCUMENTS_URL + "/" + props.pdfFile;

    const [isRendered, setRendered] = React.useState(false);
    const [numPages, setNumPages] = React.useState(0);
    const [currentPage, _] = React.useState(1);

    React.useEffect(() => {
        if (isRendered) {
            return;
        }

        pdfjsLib.getDocument(url).promise.then((doc: any) => {
            doc.getPage(currentPage).then((page: any) => {
                setNumPages(doc._pdfInfo.numPages);

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
                    setRendered(true);
                });
            });
        });

    }, [ isRendered, currentPage ]);

    return (
        <Box>
            {numPages}
            <canvas id={handleId}></canvas>
        </Box>
    );
}
