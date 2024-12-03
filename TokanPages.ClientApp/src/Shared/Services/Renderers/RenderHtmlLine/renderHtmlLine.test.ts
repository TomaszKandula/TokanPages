import "../../../../setupTests";
import { RenderHtmlLine } from "..";

describe("verify RenderHtmlLine method", () => {
    it("should render HTML line, when '<p>' tag is provided.", () => {
        // Arrange
        const expectation: string = "<p>This is test item</p>";
        const input = {
            tag: "p",
            text: "This is test item",
        };

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should render HTML line, when '<b>' tag provided.", () => {
        // Arrange
        const expectation: string = "<b>This is test item</b>";
        const input = {
            tag: "b",
            text: "This is test item",
        };

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should render HTML line, when '<ul>' tag provided.", () => {
        // Arrange
        const expectation: string = "<ul>This is test item</ul>";
        const input = {
            tag: "ul",
            text: "This is test item",
        };

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });
    
    it("should render HTML line, when '<ol>' tag provided.", () => {
        // Arrange
        const expectation: string = "<ol>This is test item</ol>";
        const input = {
            tag: "ol",
            text: "This is test item",
        };

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should render HTML line, when '<a>' tag provided.", () => {
        // Arrange
        const expectation: string = "<a>Google</a>";
        const input = {
            tag: "a",
            text: "Google",
        };

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should render HTML line, when '<u>' tag provided.", () => {
        // Arrange
        const expectation: string = "<u>This is test item</u>";
        const input = {
            tag: "u",
            text: "This is test item",
        };

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should render HTML line, when '<i>' tag provided.", () => {
        // Arrange
        const expectation: string = "<i>This is test item</i>";
        const input = {
            tag: "i",
            text: "This is test item",
        };

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should render HTML line, when '<div>' tag provided.", () => {
        // Arrange
        const expectation: string = "<div>This is test item</div>";
        const input = {
            tag: "div",
            text: "This is test item",
        };

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should render HTML line, when '<span>' tag provided.", () => {
        // Arrange
        const expectation: string = "<span>This is test item</span>";
        const input = {
            tag: "span",
            text: "This is test item",
        };

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should return whitespace, when HTML tag have undefined text.", () => {
        // Arrange
        const expectation = " ";
        const input = {
            tag: "li",
            text: undefined,
        };

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
