import * as React from "react";

//TODO: refactor for final implementation

interface PdfViewerViewProps {
    pdfFile: string;
}

const pdfWorkerUrl = "https://cdn.jsdelivr.net/npm/pdfjs-dist@3.11.174/build/pdf.worker.min.js";

export const PdfViewerView = (_: PdfViewerViewProps): JSX.Element => {
    //@ts-expect-error
    let { pdfjsLib } = globalThis;
    pdfjsLib.GlobalWorkerOptions.workerSrc = pdfWorkerUrl;

    const testUrl = "http://localhost:7000/api/v1.0/content/assets/getNonVideoAsset?blobName=documents/tomasz_tom_kandula_resume.pdf";

    React.useEffect(() => {
        pdfjsLib.getDocument(testUrl).promise.then((doc: any) => {
            console.log(`This document has ${doc._pdfInfo.numPages} pages.`);
            doc.getPage(1).then((page: any) => {
                const scale = 2.0;
                const viewport = page.getViewport({ scale: scale });
                let canvas = document.querySelector("#pdf-canvas");

                //@ts-expect-error
                const context = canvas.getContext("2d");
                //@ts-expect-error
                canvas.height = viewport.height;
                //@ts-expect-error
                canvas.width = viewport.width;

                const renderContext = {
                    canvasContext: context,
                    viewport: viewport
                };

                const renderTask = page.render(renderContext);
                renderTask.promise.then(() => {
                    console.log("Page rendered");
                });
            });
        });
    }, []);

    return (
        <div>
            <canvas id="pdf-canvas"></canvas>
        </div>
    );
}
