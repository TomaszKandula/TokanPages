import * as React from "react";
import { Document, Page, pdfjs } from "react-pdf";

const pdfWorker = "pdfjs-dist/build/pdf.worker.min.mjs";

interface PdfViewerViewProps {
    pdfFile: string;
}

export const PdfViewerView = (props: PdfViewerViewProps): JSX.Element => {
    pdfjs.GlobalWorkerOptions.workerSrc = new URL(pdfWorker, import.meta.url).toString();

    const [numPages, setNumPages] = React.useState<number>();
    const [pageNumber, _] = React.useState<number>(1);    

    const onDocumentLoadSuccess = React.useCallback(() => {
        setNumPages(numPages);
    }, []);

    return (
        <Document file={props.pdfFile} onLoadSuccess={onDocumentLoadSuccess} >
            <Page pageNumber={pageNumber} />
        </Document>
    );
}
