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

const pdfWorkerUrl = `${window.location.origin}/${PDF_WORKER_URL}`;
const pdfJsUrl = `${window.location.origin}/${PDF_JS_MIN_URL}`;

export const PdfViewer = (props: PdfViewerProps): React.ReactElement => {
    const dispatch = useDispatch();

    const hasNoFile = Validate.isEmpty(props.pdfFile);
    const url = `${GET_DOCUMENTS_URL}/${props.pdfFile}`;

    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const template = data?.components?.templates?.templates?.application;
    const content = data?.components?.pagePdfViewer;
    const unexpectedStatus = template.unexpectedStatus;

    const hasTemplates =
        template.nullError !== "" &&
        template.unexpectedError !== "" &&
        template.unexpectedStatus !== "" &&
        template.validationError !== "";

    const [isPdfMounted, setIsPdfMounted] = React.useState(false);
    const [isDocLoading, setDocLoading] = React.useState(true);
    const [hasPdfError, setPdfError] = React.useState(false);
    const [hasPdfWorkerError, setPdfWorkerError] = React.useState(false);
    const [pdfDocument, setPdfDocument] = React.useState<any>(null);
    const [numPages, setNumPages] = React.useState(0);
    const [currentPage, setCurrentPage] = React.useState(0);

    const getDocument = React.useCallback(async (pdfjsLib: any) => {
        try {
            pdfjsLib.GlobalWorkerOptions.workerSrc = pdfWorkerUrl;
            const doc = await pdfjsLib.getDocument(url).promise;

            setNumPages(doc._pdfInfo.numPages);
            setPdfDocument(doc);
            setDocLoading(false);
            setCurrentPage(1);
        } catch (error: any) {
            const statusCode = error.status.toString();
            const statusText = unexpectedStatus.replace("{STATUS_CODE}", statusCode);
            setPdfWorkerError(true);
            RaiseError({
                dispatch: dispatch,
                errorObject: statusText,
                content: template,
            });
        }
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

    // NOTE: pdf.min.js must be already loaded to use pdfjsLib object.
    React.useEffect(() => {
        if (isPdfMounted && isDocLoading && hasTemplates && !hasNoFile) {
            // @ts-expect-error
            const { pdfjsLib } = globalThis;
            getDocument(pdfjsLib);
        }
    }, [isPdfMounted, isDocLoading, hasTemplates, hasNoFile]);

    // NOTE: Load pdf.min.js an internally placed JS library
    // from Mozilla Foundation before rendering PdfViewer.
    React.useEffect(() => {
        const script = document.createElement("script");
        script.src = pdfJsUrl;
        script.async = true;

        script.onload = () => {
            setIsPdfMounted(true);
        };

        script.onerror = () => { 
            setPdfError(true);
        };

        document.body.appendChild(script);

        return () => {
            document.body.removeChild(script);
        };
    }, []);

    return !isPdfMounted && !hasPdfError ? (
        <ProgressBar classNameWrapper="pt-96" />
    ) : (
        <PdfViewerView
            isDocLoading={isDocLoading}
            hasNoFilePrompt={hasNoFile}
            hasPdfError={hasPdfError}
            hasPdfWorkerError={hasPdfWorkerError}
            content={{
                isLoading: data?.isLoading,
                caption: content?.caption,
                warning: content?.text,
                error: template.unexpectedError,
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
