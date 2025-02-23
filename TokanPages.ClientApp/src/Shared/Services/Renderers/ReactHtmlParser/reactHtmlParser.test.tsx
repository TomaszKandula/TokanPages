import "../../../../setupTests";
import * as React from "react";
import { render, screen } from "@testing-library/react";
import { ReactHtmlParser } from "./reactHtmlParser";

const setupUrl = (url: string) => {
    window = Object.create(window);
    Object.defineProperty(window, "location", {
        value: { href: url }, writable: true
    });
}

describe("verify ReactHtmlParser method", () => {
    it("should sanitize unsafe HTML when snapshot mode is ON", () => {
        // Arrange
        setupUrl("http://localhost/snapshot/en");

        const unsafe = "<script>var sum=1+1;</script>";
        const safe = `<div data-testid="safe"><p>Test paragrapth</p><a href="http://google.com" rel="noopener" target="_blank">Google</a></div>`;
        const html = `<div data-testid="container">${unsafe}${safe}</div>`;
 
        // Act
        render(<ReactHtmlParser html={html} />);

        // Assert
        const container = screen.getByTestId("container");
        const child = screen.getByTestId("safe");
        expect(container).not.toContainHTML(unsafe);
        expect(container).toContainElement(child);
    });

    it("should sanitize unsafe HTML", () => {
        // Arrange
        setupUrl("http://localhost/en");

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
        setupUrl("http://localhost/en");

        const text = "Test item";
        const unsafe = `<div data-testid="disallowed-data-attribute">${text}</div>`;
        const expected = `<div>${text}</div>`;

        // Act
        render(<ReactHtmlParser html={unsafe} />);

        // Assert
        const rendered = screen.getByText("Test item");
        expect(rendered).toContainHTML(expected);
    });

    it("should return HTML wrap around given HTML compoment: <h1>", () => {
        // Arrange
        setupUrl("http://localhost/en");

        const input = "Test item";
        const output = `<h1>${input}</h1>`;

        // Act
        render(<ReactHtmlParser html={input} allowDataAttr={true} component="h1" />);

        // Assert
        const container = screen.getByText(input);
        expect(container).toContainHTML(output);
    });

    it("should return HTML wrap around given HTML compoment: <h2>", () => {
        // Arrange
        setupUrl("http://localhost/en");

        const input = "Test item";
        const output = `<h2>${input}</h2>`;

        // Act
        render(<ReactHtmlParser html={input} allowDataAttr={true} component="h2" />);

        // Assert
        const container = screen.getByText(input);
        expect(container).toContainHTML(output);
    });

    it("should return HTML wrap around given HTML compoment: <h3>", () => {
        // Arrange
        setupUrl("http://localhost/en");

        const input = "Test item";
        const output = `<h3>${input}</h3>`;

        // Act
        render(<ReactHtmlParser html={input} allowDataAttr={true} component="h3" />);

        // Assert
        const container = screen.getByText(input);
        expect(container).toContainHTML(output);
    });

    it("should return HTML wrap around given HTML compoment: <h4>", () => {
        // Arrange
        setupUrl("http://localhost/en");

        const input = "Test item";
        const output = `<h4>${input}</h4>`;

        // Act
        render(<ReactHtmlParser html={input} allowDataAttr={true} component="h4" />);

        // Assert
        const container = screen.getByText(input);
        expect(container).toContainHTML(output);
    });

    it("should return HTML wrap around given HTML compoment: <h5>", () => {
        // Arrange
        setupUrl("http://localhost/en");

        const input = "Test item";
        const output = `<h5>${input}</h5>`;

        // Act
        render(<ReactHtmlParser html={input} allowDataAttr={true} component="h5" />);

        // Assert
        const container = screen.getByText(input);
        expect(container).toContainHTML(output);
    });

    it("should return HTML wrap around given HTML compoment: <h6>", () => {
        // Arrange
        setupUrl("http://localhost/en");

        const input = "Test item";
        const output = `<h6>${input}</h6>`;

        // Act
        render(<ReactHtmlParser html={input} allowDataAttr={true} component="h6" />);

        // Assert
        const container = screen.getByText(input);
        expect(container).toContainHTML(output);
    });

    it("should return HTML wrap around given HTML compoment: <p>", () => {
        // Arrange
        setupUrl("http://localhost/en");

        const input = "Test item";
        const output = `<p>${input}</p>`;

        // Act
        render(<ReactHtmlParser html={input} allowDataAttr={true} component="p" />);

        // Assert
        const container = screen.getByText(input);
        expect(container).toContainHTML(output);
    });

    it("should return HTML wrap around given HTML compoment: <span>", () => {
        // Arrange
        setupUrl("http://localhost/en");

        const input = "Test item";
        const output = `<span>${input}</span>`;

        // Act
        render(<ReactHtmlParser html={input} allowDataAttr={true} component="span" />);

        // Assert
        const container = screen.getByText(input);
        expect(container).toContainHTML(output);
    });

    it("should return HTML wrap around given HTML compoment: <div>", () => {
        // Arrange
        setupUrl("http://localhost/en");

        const input = "Test item";
        const output = `<div>${input}</div>`;

        // Act
        render(<ReactHtmlParser html={input} allowDataAttr={true} component="div" />);

        // Assert
        const container = screen.getByText(input);
        expect(container).toContainHTML(output);
    });
});
