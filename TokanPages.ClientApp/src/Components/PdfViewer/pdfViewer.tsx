import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { GET_DOCUMENTS_URL } from "../../Api/Request";
import { RaiseError } from "../../Shared/Services/ErrorServices";
import { PDF_WORKER_URL } from "../../Shared/constants";
import { PdfViewerView } from "./View/pdfViewerView";

interface PdfViewerProps {
    pdfFile: string;
    scale?: number;
    background?: React.CSSProperties;
}

export const PdfViewer = (props: PdfViewerProps): JSX.Element => {
    //@ts-expect-error
    let { pdfjsLib } = globalThis;
    pdfjsLib.GlobalWorkerOptions.workerSrc = PDF_WORKER_URL;
    const url = `${GET_DOCUMENTS_URL}/${props.pdfFile}`;

    const dispatch = useDispatch();

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
        <PdfViewerView 
            isLoading={isLoading}
            hasError={hasError}
            currentPage={currentPage}
            numPages={numPages}
            pdfDocument={pdfDocument}
            scale={props.scale}
            background={props.background}
            previousPage={previousPage}
            nextPage={nextPage}
        />
    );
};
