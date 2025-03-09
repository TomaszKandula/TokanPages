import "../../../setupTests";
import React from "react";
import { render, screen } from "@testing-library/react";
import { RenderList } from "./renderList";

describe("test render function 'RenderList'", () => {
    it("should return rendered list for provided 'ul' type.", () => {
        // Arrange
        const list = ["item1", "item2", "item3"]
        const expected = "<ul data-testid=\"test-list\" class=\"list-box\"><li>item1</li><li>item2</li><li>item3</li></ul>";

        // Act
        render(<RenderList list={list} type="ul" dataTestId="test-list" />);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toMatchSnapshot(expected);
    });

    it("should return rendered list for provided 'ul' type with 'a' element.", () => {
        // Arrange
        const list = ["item1 __{\"href\":\"https://test.com\",\"target\":\"_blank\",\"rel\":\"noopener\",\"text\":\"Test\"}__"]
        const expected = "<ul class=\"list-box\" data-testid=\"test-list\"><li>item1 <a href=\"https://test.com\" rel=\"noopener\" target=\"_blank\">Test</a></li></ul>";

        // Act
        render(<RenderList list={list} type="ul" dataTestId="test-list" />);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toMatchSnapshot(expected);
    });

    it("should return rendered list for provided 'ol' type.", () => {
        // Arrange
        const list = ["item1", "item2", "item3"]
        const expected = "<ol data-testid=\"test-list\" class=\"list-box\"><li>item1</li><li>item2</li><li>item3</li></ol>";

        // Act
        render(<RenderList list={list} type="ol" dataTestId="test-list" />);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toMatchSnapshot(expected);
    });

    it("should return rendered list for provided 'ol' type with 'a' element.", () => {
        // Arrange
        const list = ["item1 __{\"href\":\"https://test.com\",\"target\":\"_blank\",\"rel\":\"noopener\",\"text\":\"Test\"}__"]
        const expected = "<ol class=\"list-box\" data-testid=\"test-list\"><li>item1 <a href=\"https://test.com\" rel=\"noopener\" target=\"_blank\">Test</a></li></ol>";

        // Act
        render(<RenderList list={list} type="ol" dataTestId="test-list" />);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toMatchSnapshot(expected);
    });

    it("should return rendered 'ul' list when no type is provided.", () => {
        // Arrange
        const list = ["item1", "item2", "item3"]
        const expected = "<ul class=\"list-box\" data-testid=\"test-list\"><li>item1</li><li>item2</li><li>item3</li></ul>";

        // Act
        render(<RenderList list={list} dataTestId="test-list" />);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toMatchSnapshot(expected);
    });

    it("should return no list when empty list is provided.", () => {
        // Arrange
        const list = [""]
        const expected = "<ul data-testid=\"test-list\" class=\"list-box\"><li>EMPTY_CONTENT_PROVIDED</li></ul>";

        // Act
        render(<RenderList list={list} dataTestId="test-list" />);

        // Assert
        const container = screen.getByTestId("test-list");
        expect(container).toMatchSnapshot(expected);
    });
});
