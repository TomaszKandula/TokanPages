import * as React from "react";
import { Box, Container, Grid } from "@material-ui/core";
import NavigateBeforeIcon from "@material-ui/icons/NavigateBefore";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import ReportProblemIcon from "@material-ui/icons/ReportProblem";
import { BackArrow, DownloadAsset, PdfCanvas, ProgressBar } from "../../../Shared/Components";
import { PdfViewerStyle } from "./pdfViewerStyle";

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

const RenderIconOrLoading = (props: RenderIconOrErrorProps): JSX.Element => {
    return props.isLoading && !props.hasError ? <ProgressBar size={20} /> : <RenderIcon {...props} />;
};

export const PdfViewerView = (props: PdfViewerViewProps): JSX.Element => {
    const classes = PdfViewerStyle();
    return (
        <section className={classes.section} style={props.background}>
            <Container className={classes.container}>
                <Box pt={4} pb={6}>
                    <Box pt={0} pb={6}>
                        <BackArrow className={classes.back_arrow} />
                    </Box>
                    <Grid container justifyContent="center" direction="column">
                        <Box pt={2} pb={2} className={classes.header}>
                            <RenderIconOrLoading
                                isLoading={props.isLoading}
                                hasError={props.hasError}
                                pdfUrl={props.pdfUrl}
                            />
                            <div className={classes.header_pages}>
                                {props.currentPage} / {props.numPages}
                            </div>
                            <div>
                                <NavigateBeforeIcon className={classes.header_buttons} onClick={props.onPreviousPage} />
                                <NavigateNextIcon className={classes.header_buttons} onClick={props.onNextPage} />
                            </div>
                        </Box>
                        <Box className={classes.canvasWrapper}>
                            <PdfCanvas
                                pdfDocument={props.pdfDocument}
                                pageNumber={props.currentPage}
                                scale={props.scale ?? 1.5}
                                htmlAttributes={{ className: classes.canvas }}
                            />
                        </Box>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
};
