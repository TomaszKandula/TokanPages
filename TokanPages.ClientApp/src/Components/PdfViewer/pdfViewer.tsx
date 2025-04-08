import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { GET_DOCUMENTS_URL } from "../../Api/Request";
import { RaiseError } from "../../Shared/Services/ErrorServices";
import { ProgressBar } from "../../Shared/Components";
import { PDF_JS_MIN_URL, PDF_WORKER_URL } from "../../Shared/constants";
import { PdfViewerView } from "./View/pdfViewerView";
import Validate from "validate.js";

interface PdfViewerProps {
    pdfFile: string;
    scale?: number;
    background?: string;
}

export const PdfViewer = (props: PdfViewerProps): React.ReactElement => {
    const dispatch = useDispatch();

    const hasNoFile = Validate.isEmpty(props.pdfFile);
    const url = `${GET_DOCUMENTS_URL}/${props.pdfFile}`;

    const content = useSelector((state: ApplicationState) => state.contentPageData.components.pagePdfViewer);
    const template = useSelector(
        (state: ApplicationState) => state.contentPageData.components.templates.templates.application
    );

    const hasTemplates =
        template.nullError !== "" &&
        template.unexpectedError !== "" &&
        template.unexpectedStatus !== "" &&
        template.validationError !== "";

    const [isPdfMounted, setIsPdfMounted] = React.useState(false);
    const [isLoading, setLoading] = React.useState(true);
    const [hasError, setError] = React.useState(false);
    const [pdfDocument, setPdfDocument] = React.useState<any>(null);
    const [numPages, setNumPages] = React.useState(0);
    const [currentPage, setCurrentPage] = React.useState(0);

    const getDocument = React.useCallback(async () => {
        // @ts-expect-error
        // NOTE: pdf.min.js must be already loaded to use pdfjsLib object.
        let { pdfjsLib } = globalThis;
        if (!pdfjsLib) {
            return;
        }

        pdfjsLib.GlobalWorkerOptions.workerSrc = PDF_WORKER_URL;
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

    const onNextPageHandler = React.useCallback(() => {
        if (numPages === currentPage) {
            return;
        }

        const next = currentPage + 1;
        setCurrentPage(next);
    }, [numPages, currentPage]);

    const onPreviousPageHandler = React.useCallback(() => {
        if (currentPage === 1) {
            return;
        }

        const previous = currentPage - 1;
        setCurrentPage(previous);
    }, [numPages, currentPage]);

    React.useEffect(() => {
        if (isPdfMounted && isLoading && hasTemplates && !hasNoFile) {
            getDocument();
        }
    }, [isPdfMounted, isLoading, hasTemplates, hasNoFile]);

    // NOTE: Load pdf.min.js an internally placed JS library
    // from Mozilla Foundation before rendering PdfViewer.
    React.useEffect(() => {
        const script = document.createElement("script");
        script.src = PDF_JS_MIN_URL;
        script.async = true;

        document.body.appendChild(script);

        setTimeout(() => {
            setIsPdfMounted(true);
        }, 500);

        return () => {
            document.body.removeChild(script);
        };
    }, []);

    return !isPdfMounted ? (
        <ProgressBar classNameWrapper="pt-96" />
    ) : (
        <PdfViewerView
            isLoading={isLoading}
            hasNoFilePrompt={hasNoFile}
            hasError={hasError}
            content={{
                caption: content?.caption ?? "",
                text: content?.text ?? "",
            }}
            currentPage={currentPage}
            numPages={numPages}
            pdfDocument={pdfDocument}
            scale={props.scale}
            background={props.background}
            pdfUrl={url}
            onPreviousPage={onPreviousPageHandler}
            onNextPage={onNextPageHandler}
        />
    );
};
