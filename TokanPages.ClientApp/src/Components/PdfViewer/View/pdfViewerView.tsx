import * as React from "react";
import { Card, DownloadAsset, Icon, IconButton, PdfCanvas, ProgressBar } from "../../../Shared/Components";
import { PdfViewerViewProps, RenderIconOrErrorProps } from "../Types";
import "./pdfViewerView.css";

const RenderIcon = (props: RenderIconOrErrorProps) => {
    return props.hasPdfWorkerError ? (
        <Icon name="AlertOctagon" size={2} />
    ) : (
        <DownloadAsset url={props.pdfUrl ?? ""} size={1.5} className="has-text-black" />
    );
};

const RenderIconOrLoading = (props: RenderIconOrErrorProps): React.ReactElement => {
    return props.isDocLoading && !props.hasPdfWorkerError ? <ProgressBar size={20} /> : <RenderIcon {...props} />;
};

const RenderDocument = (props: PdfViewerViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-desktop mb-6">
            <div className="py-6">
                <div className="bulma-card m-4">
                    <div className="bulma-card-content p-0">
                        <div className="is-flex is-justify-content-space-around is-align-items-center py-4">
                            <RenderIconOrLoading
                                isDocLoading={props.isDocLoading}
                                hasPdfWorkerError={props.hasPdfWorkerError}
                                pdfUrl={props.pdfUrl}
                            />
                            <p className="is-size-6 has-text-weight-semibold is-flex is-align-self-center">
                                {props.currentPage} / {props.numPages}
                            </p>
                            <div className="is-flex is-gap-1.5">
                                <IconButton
                                    size={2.5}
                                    hasGreyBackground
                                    className="no-select"
                                    onClick={props.onPreviousPage}
                                >
                                    <Icon name="ChevronLeft" size={1.5} className="has-text-black" />
                                </IconButton>
                                <IconButton
                                    size={2.5}
                                    hasGreyBackground
                                    className="no-select"
                                    onClick={props.onNextPage}
                                >
                                    <Icon name="ChevronRight" size={1.5} className="has-text-black" />
                                </IconButton>
                            </div>
                        </div>
                        <hr className="m-0" />
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
        <div className="bulma-container bulma-is-max-tablet mb-6">
            <div className="py-6">
                <Card
                    isLoading={props?.content?.isLoading}
                    caption={props?.content?.caption}
                    text={[props?.content?.warning]}
                    icon={<Icon name="FileDocument" size={4.5} />}
                    colour="has-text-info"
                />
            </div>
        </div>
    </section>
);

const RenderPdfErrorPrompt = (props: PdfViewerViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet mb-6">
            <div className="py-6">
                <Card
                    isLoading={props?.content?.isLoading}
                    caption={props?.content?.caption}
                    text={[props?.content?.error]}
                    icon={<Icon name="AlertCircle" size={4.5} />}
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
