import "../../../setupTests";
import React from "react";
import { render, screen } from "@testing-library/react";
import { RenderList } from "./renderList";

describe("test render function 'RenderList'", () => {
    it("should return rendered list for provided 'ul' type.", () => {
        // Arrange
        const list = ["item1", "item2", "item3"]
        const expected = "<ul class='list-box'><li>item1</li><li>item2</li><li>item3</li></ul>";
        
        // Act
        render(<span data-testid="test-list"><RenderList list={list} type="ul" /></span>);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toContainHTML(expected);
    });

    it("should return rendered list for provided 'ol' type.", () => {
        // Arrange
        const list = ["item1", "item2", "item3"]
        const expected = "<ol class='list-box'><li>item1</li><li>item2</li><li>item3</li></ol>";
        
        // Act
        render(<span data-testid="test-list"><RenderList list={list} type="ol" /></span>);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toContainHTML(expected);
    });

    it("should return rendered UL list when no type is provided.", () => {
        // Arrange
        const list = ["item1", "item2", "item3"]
        const expected = "<ul class='list-box'><li>item1</li><li>item2</li><li>item3</li></ul>";
        
        // Act
        render(<span data-testid="test-list"><RenderList list={list} /></span>);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toContainHTML(expected);
    });

    it("should return no list when empty list is provided.", () => {
        // Arrange
        const list = [""]
        const expected = "<ul class='list-box'><li /></ul>";
        
        // Act
        render(<span data-testid="test-list"><RenderList list={list} /></span>);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toContainHTML(expected);
    });
});
