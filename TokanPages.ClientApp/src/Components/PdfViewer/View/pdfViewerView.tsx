import * as React from "react";
import { Box, Container, Grid } from "@material-ui/core";
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
            <Container className="container-super-wide">
                <Box pt={10} pb={6}>
                    <Grid container justifyContent="center" direction="column">
                        <Box pt={2} pb={2} className="pdf-header">
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
                        </Box>
                        <Box className="pdf-canvas-wrapper">
                            <PdfCanvas
                                pdfDocument={props.pdfDocument}
                                pageNumber={props.currentPage}
                                scale={props.scale ?? 1.5}
                                htmlAttributes={{ className: "pdf-canvas" }}
                            />
                        </Box>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
};
