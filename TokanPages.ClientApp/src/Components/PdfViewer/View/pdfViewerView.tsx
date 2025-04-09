import * as React from "react";
import { Card, CardContent, Container } from "@material-ui/core";
import NavigateBeforeIcon from "@material-ui/icons/NavigateBefore";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import ReportProblemIcon from "@material-ui/icons/ReportProblem";
import DescriptionIcon from "@material-ui/icons/Description";
import ErrorIcon from "@material-ui/icons/Error";
import { CustomCard, DownloadAsset, PdfCanvas, ProgressBar } from "../../../Shared/Components";

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
    background?: string;
    onPreviousPage?: React.MouseEventHandler<SVGSVGElement>;
    onNextPage?: React.MouseEventHandler<SVGSVGElement>;
}

interface RenderIconOrErrorProps {
    isDocLoading: boolean;
    hasPdfWorkerError: boolean;
    pdfUrl?: string;
}

const RenderIcon = (props: RenderIconOrErrorProps) => {
    return props.hasPdfWorkerError ? <ReportProblemIcon /> : <DownloadAsset url={props.pdfUrl ?? ""} />;
};

const RenderIconOrLoading = (props: RenderIconOrErrorProps): React.ReactElement => {
    return props.isDocLoading && !props.hasPdfWorkerError ? <ProgressBar size={20} /> : <RenderIcon {...props} />;
};

const RenderDocument = (props: PdfViewerViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-wide-1000">
                <div className="pt-80 pb-48">
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div className="pdf-header">
                                <div className="pdf-header-download-icon">
                                    <RenderIconOrLoading
                                        isDocLoading={props.isDocLoading}
                                        hasPdfWorkerError={props.hasPdfWorkerError}
                                        pdfUrl={props.pdfUrl}
                                    />
                                </div>
                                <div className="pdf-header-pages">
                                    {props.currentPage} / {props.numPages}
                                </div>
                                <div className="pdf-header-buttons-container">
                                    <NavigateBeforeIcon className="pdf-header-buttons" onClick={props.onPreviousPage} />
                                    <NavigateNextIcon className="pdf-header-buttons" onClick={props.onNextPage} />
                                </div>
                            </div>
                            <div className="pdf-canvas-wrapper">
                                <PdfCanvas
                                    pdfDocument={props.pdfDocument}
                                    pageNumber={props.currentPage}
                                    scale={props.scale ?? 1.5}
                                    htmlAttributes={{ className: "pdf-canvas" }}
                                />
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
}

const RenderNoDocumentPrompt = (props: PdfViewerViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-wide">
                <div className="pt-80 pb-48">
                    <CustomCard 
                        isLoading={props?.content?.isLoading}
                        caption={props?.content?.caption}
                        text={[props?.content?.warning]}
                        icon={<DescriptionIcon />}
                        colour="info"
                    />
                </div>
            </Container>
        </section>
    );
}

const RenderPdfErrorPrompt = (props: PdfViewerViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-wide">
                <div className="pt-80 pb-48">
                    <CustomCard 
                        isLoading={props?.content?.isLoading}
                        caption={props?.content?.caption}
                        text={[props?.content?.error]}
                        icon={<ErrorIcon />}
                        colour="error"
                    />
                </div>
            </Container>
        </section>
    );
}

export const PdfViewerView = (props: PdfViewerViewProps): React.ReactElement => {
    if (props.hasPdfError) {
        return <RenderPdfErrorPrompt {...props} />;
    }

    if (props.hasNoFilePrompt) {
        return <RenderNoDocumentPrompt {...props} />;
    }

    return <RenderDocument {...props} />;
};
