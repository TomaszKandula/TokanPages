import "../../../setupTests";
import React from "react";
import { render, screen } from "@testing-library/react";
import { RenderParagraphs } from "./renderParagraphs";

describe("test render function 'RenderParagraphs'", () => {
    it("should return rendered text paragraphs for given text items.", () => {
        // Arrange
        const list = ["item1", "item2", "item3"]
        const output = `
            <span data-testid="test-list">
                <p class="MuiTypography-root MuiTypography-body1">item1</p>
                <p class="MuiTypography-root MuiTypography-body1">item2</p>
                <p class="MuiTypography-root MuiTypography-body1">item3</p>
            </span>`;

        const expected = output.replace(/\s+/g, " ").replaceAll(" <", "<");

        // Act
        render(<span data-testid="test-list"><RenderParagraphs text={list} /></span>);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toContainHTML(expected);
    });
});
