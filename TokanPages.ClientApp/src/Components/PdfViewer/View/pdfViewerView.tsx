import * as React from "react";
import { Card, CardContent, Container } from "@material-ui/core";
import NavigateBeforeIcon from "@material-ui/icons/NavigateBefore";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import ReportProblemIcon from "@material-ui/icons/ReportProblem";
import { DownloadAsset, PdfCanvas, ProgressBar } from "../../../Shared/Components";

interface PdfViewerViewProps {
    isLoading: boolean;
    hasError: boolean;
    currentPage: number;
    numPages: number;
    pdfDocument: any;
    scale?: number;
    pdfUrl?: string;
    background?: React.CSSProperties;
    onPreviousPage?: React.MouseEventHandler<SVGSVGElement>;
    onNextPage?: React.MouseEventHandler<SVGSVGElement>;
}

interface RenderIconOrErrorProps {
    isLoading: boolean;
    hasError: boolean;
    pdfUrl?: string;
}

const RenderIcon = (props: RenderIconOrErrorProps) => {
    return props.hasError ? <ReportProblemIcon /> : <DownloadAsset url={props.pdfUrl ?? ""} />;
};

const RenderIconOrLoading = (props: RenderIconOrErrorProps): React.ReactElement => {
    return props.isLoading && !props.hasError ? <ProgressBar size={20} /> : <RenderIcon {...props} />;
};

export const PdfViewerView = (props: PdfViewerViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container-wide-1000">
                <div style={{ paddingTop: 80, paddingBottom: 48 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div className="pdf-header">
                                <RenderIconOrLoading
                                    isLoading={props.isLoading}
                                    hasError={props.hasError}
                                    pdfUrl={props.pdfUrl}
                                />
                                <div className="pdf-header-pages">
                                    {props.currentPage} / {props.numPages}
                                </div>
                                <div>
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
};
