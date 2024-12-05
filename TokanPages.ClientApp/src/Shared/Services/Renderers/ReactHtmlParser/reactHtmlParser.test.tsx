import "../../../../setupTests";
import * as React from "react";
import { render, screen } from "@testing-library/react";
import { ReactHtmlParser } from "./reactHtmlParser";

describe("verify ReactHtmlParser method", () => {
    it("should sanitize unsafe HTML", () => {
        // Arrange
        const unsafe = "<script>var sum=1+1;</script>";
        const safe = `<div data-testid="safe"><p>Test paragrapth</p><a href="http://google.com" rel="noopener" target="_blank">Google</a></div>`;
        const html = `<div data-testid="container">${unsafe}${safe}</div>`;

        // Act
        render(<ReactHtmlParser html={html} allowDataAttr={true} />);

        // Assert
        const container = screen.getByTestId("container");
        const child = screen.getByTestId("safe");
        expect(container).not.toContainHTML(unsafe);
        expect(container).toContainElement(child);
    });

    it("should sanitize unsafe HTML, disallow data attribute", () => {
        // Arrange
        const text = "Test item";
        const unsafe = `<div data-testid="disallowed-data-attribute">${text}</div>`;
        const expected = `<div>${text}</div>`;

        // Act
        render(<ReactHtmlParser html={unsafe} />);

        // Assert
        const rendered = screen.getByText("Test item");
        expect(rendered).toContainHTML(expected);
    });
});
