import { ReactMouseEvent } from "../../../Shared/Types";

export interface PdfViewerProps {
    pdfFile: string;
    scale?: number;
    className?: string;
}

export interface PdfViewerViewProps {
    isDocLoading: boolean;
    hasNoFilePrompt: boolean;
    hasPdfError: boolean;
    hasPdfWorkerError: boolean;
    content: {
        isLoading: boolean;
        caption: string;
        warning: string;
        error: string;
    };
    currentPage: number;
    numPages: number;
    pdfDocument: any;
    scale?: number;
    pdfUrl?: string;
    className?: string;
    onPreviousPage?: (event: ReactMouseEvent) => void;
    onNextPage?: (event: ReactMouseEvent) => void;
}

export interface RenderIconOrErrorProps {
    isDocLoading: boolean;
    hasPdfWorkerError: boolean;
    pdfUrl?: string;
}
