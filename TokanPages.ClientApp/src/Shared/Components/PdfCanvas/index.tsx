import * as React from "react";

interface PdfCanvasProps {
    pdfDocument: any;
    pageNumber: number;
    scale: number;
    htmlAttributes: React.HTMLAttributes<HTMLCanvasElement>;
}

export const PdfCanvas = (props: PdfCanvasProps): React.ReactElement => {
    const canvasRef = React.useRef<HTMLCanvasElement | null>(null);

    const renderPage = React.useCallback(
        async (numPage: number, canvas: HTMLCanvasElement, context: CanvasRenderingContext2D) => {
            const page = await props.pdfDocument.getPage(numPage);
            const viewport = page.getViewport({ scale: props.scale });

            canvas.height = viewport.height;
            canvas.width = viewport.width;

            const renderContext = { canvasContext: context, viewport: viewport };
            const renderTask = page.render(renderContext);

            renderTask.promise.then(() => {
                renderTask.cancel();
            });
        },
        [props.pdfDocument]
    );

    React.useEffect(() => {
        const canvas = canvasRef.current;
        if (canvas === null) {
            return;
        }

        const context = canvas.getContext("2d");
        if (context === null) {
            return;
        }

        if (props.pdfDocument !== null && props.pageNumber > 0) {
            renderPage(props.pageNumber, canvas, context);
        }
    }, [props.pdfDocument, props.pageNumber]);

    return <canvas ref={canvasRef} {...props.htmlAttributes} />;
};
