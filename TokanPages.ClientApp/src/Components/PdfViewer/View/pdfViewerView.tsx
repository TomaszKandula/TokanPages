import * as React from "react";
import { CustomCard, DownloadAsset, Icon, IconButton, PdfCanvas, ProgressBar } from "../../../Shared/Components";
import { ReactMouseEvent } from "../../../Shared/types";
import "./pdfViewerView.css";

interface PdfViewerViewProps {
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

interface RenderIconOrErrorProps {
    isDocLoading: boolean;
    hasPdfWorkerError: boolean;
    pdfUrl?: string;
}

const RenderIcon = (props: RenderIconOrErrorProps) => {
    return props.hasPdfWorkerError ? (
        <Icon name="AlertOctagon" size={3} />
    ) : (
        <DownloadAsset url={props.pdfUrl ?? ""} className="has-text-black" />
    );
};

const RenderIconOrLoading = (props: RenderIconOrErrorProps): React.ReactElement => {
    return props.isDocLoading && !props.hasPdfWorkerError ? <ProgressBar size={20} /> : <RenderIcon {...props} />;
};

const RenderDocument = (props: PdfViewerViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-desktop">
            <div className="py-6">
                <div className="bulma-card m-4">
                    <div className="bulma-card-content p-0">
                        <div className="is-flex is-justify-content-space-around">
                            <RenderIconOrLoading
                                isDocLoading={props.isDocLoading}
                                hasPdfWorkerError={props.hasPdfWorkerError}
                                pdfUrl={props.pdfUrl}
                            />
                            <p className="is-size-6 has-text-weight-semibold is-flex is-align-self-center">
                                {props.currentPage} / {props.numPages}
                            </p>
                            <div className="is-flex">
                                <IconButton onClick={props.onPreviousPage}>
                                    <Icon name="ChevronLeft" size={1.1} />
                                </IconButton>
                                <IconButton onClick={props.onNextPage}>
                                    <Icon name="ChevronRight" size={1.1} />
                                </IconButton>
                            </div>
                        </div>
                        <PdfCanvas
                            pdfDocument={props.pdfDocument}
                            pageNumber={props.currentPage}
                            scale={props.scale ?? 1.5}
                            htmlAttributes={{ className: "pdf-canvas" }}
                        />
                    </div>
                </div>
            </div>
        </div>
    </section>
);

const RenderNoDocumentPrompt = (props: PdfViewerViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container">
            <div className="py-6">
                <CustomCard
                    isLoading={props?.content?.isLoading}
                    caption={props?.content?.caption}
                    text={[props?.content?.warning]}
                    icon={<Icon name="FileDocument" size={3} />}
                    colour="has-text-info"
                />
            </div>
        </div>
    </section>
);

const RenderPdfErrorPrompt = (props: PdfViewerViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container">
            <div className="py-6">
                <CustomCard
                    isLoading={props?.content?.isLoading}
                    caption={props?.content?.caption}
                    text={[props?.content?.error]}
                    icon={<Icon name="AlertCircle" size={3} />}
                    colour="has-text-danger"
                />
            </div>
        </div>
    </section>
);

export const PdfViewerView = (props: PdfViewerViewProps): React.ReactElement => {
    if (props.hasPdfError) {
        return <RenderPdfErrorPrompt {...props} />;
    }

    if (props.hasNoFilePrompt) {
        return <RenderNoDocumentPrompt {...props} />;
    }

    return <RenderDocument {...props} />;
};
