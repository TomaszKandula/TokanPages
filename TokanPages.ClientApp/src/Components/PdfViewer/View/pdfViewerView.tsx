import * as React from "react";

interface PdfViewerViewProps {
    pdfFile: string;
}

export const PdfViewerView = (_: PdfViewerViewProps): JSX.Element => {
    //@ts-expect-error
    let { pdfjsLib } = globalThis;
    pdfjsLib.GlobalWorkerOptions.workerSrc = "//mozilla.github.io/pdf.js/build/pdf.worker.mjs";

    return (
        <div>
            <canvas></canvas>
        </div>
    );
}
