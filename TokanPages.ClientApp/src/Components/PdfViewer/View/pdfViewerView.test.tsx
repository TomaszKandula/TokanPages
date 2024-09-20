import "../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { PdfViewerView } from "./pdfViewerView";

describe("test component: pdfViewerView", () => {
    it("should render correctly '<PdfViewerView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <PdfViewerView
                    isLoading={false}
                    hasError={false}
                    currentPage={1}
                    numPages={1}
                    pdfDocument={{}}
                    scale={undefined}
                    pdfUrl="https://test.com/documents/some_document.pdf"
                    onPreviousPage={jest.fn()}
                    onNextPage={jest.fn()}
                    background={{ backgroundColor: "white" }}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
