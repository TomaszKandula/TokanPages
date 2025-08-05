import "../../../../setupTests";
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

    it("should return sanitized HTML value wrapped with <div>", () => {
        render(<RenderHtml value="<p>short test HTML text</p>" tag="div" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<div data-testid="render-html"><p>short test HTML text</p></div>');
    });

    it("should return sanitized HTML value with non-breaking space", () => {
        render(<RenderHtml value="<span>I think I am the best</span>" tag="p" testId="render-html" />);

        const rendered = screen.getByTestId("render-html");

        expect(rendered).toContainHTML('<p data-testid="render-html"><span>I think I&nbsp;am the best</span></p>');
    });
});
