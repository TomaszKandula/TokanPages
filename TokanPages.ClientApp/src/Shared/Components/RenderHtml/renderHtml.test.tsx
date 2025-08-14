import "../../../setupTests";
import React from "react";
import { screen, render } from "@testing-library/react";
import { RenderHtml } from "./renderHtml";

describe("Verify 'RenderHtml' component", () => {
    it("should return same value for empty input", () => {
        render(<RenderHtml value="" tag="p" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<p data-testid="render-html" />');
    });

    it("should return same value for short non-HTML input", () => {
        render(<RenderHtml value="short text" tag="p" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<p data-testid="render-html">short text</p>');
    });

    it("should return sanitized HTML value wrapped with <p>", () => {
        render(<RenderHtml value="<span>short test HTML text</span>" tag="p" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<p data-testid="render-html"><span>short test HTML text</span></p>');
    });

    it("should return sanitized HTML value wrapped with <span>", () => {
        render(<RenderHtml value="<span>short test HTML text</span>" tag="span" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<span data-testid="render-html"><span>short test HTML text</span></span>');
    });

    it("should return sanitized HTML value wrapped with <h1>", () => {
        render(<RenderHtml value="short test text" tag="h1" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<h1 data-testid="render-html">short test text</h1>');
    });

    it("should return sanitized HTML value wrapped with <h2>", () => {
        render(<RenderHtml value="short test text" tag="h2" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<h2 data-testid="render-html">short test text</h2>');
    });

    it("should return sanitized HTML value wrapped with <h3>", () => {
        render(<RenderHtml value="short test text" tag="h3" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<h3 data-testid="render-html">short test text</h3>');
    });

    it("should return sanitized HTML value wrapped with <h4>", () => {
        render(<RenderHtml value="short test text" tag="h4" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<h4 data-testid="render-html">short test text</h4>');
    });

    it("should return sanitized HTML value wrapped with <h5>", () => {
        render(<RenderHtml value="short test text" tag="h5" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<h5 data-testid="render-html">short test text</h5>');
    });

    it("should return sanitized HTML value wrapped with <h6>", () => {
        render(<RenderHtml value="short test text" tag="h6" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<h6 data-testid="render-html">short test text</h6>');
    });

    it("should return sanitized HTML value wrapped with <blockquote>", () => {
        render(<RenderHtml value="short test text" tag="blockquote" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<blockquote data-testid="render-html">short test text</blockquote>');
    });

    it("should return sanitized HTML value wrapped with <blockquote>", () => {
        render(<RenderHtml value="short test text" tag="blockquote" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<blockquote data-testid="render-html">short test text</blockquote>');
    });

    it("should return sanitized HTML value wrapped with <li>", () => {
        render(<RenderHtml value="<p>short test HTML text</p>" tag="li" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<li data-testid="render-html"><p>short test HTML text</p></li>');
    });

    it("should return sanitized HTML value with non-breaking space", () => {
        render(<RenderHtml value="<span>I think I am the best</span>" tag="p" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<p data-testid="render-html"><span>I think I&nbsp;am the best</span></p>');
    });
});
