import "../../../../setupTests";
import { GetImageUrl } from "..";

describe("verify GetImageUrl method", () => {
    it("should return url, when base and name is provided.", () => {
        // Arrange
        const expectation: string = "http://localhost:5000/victoria.jpg";
        const input = {
            base: "http://localhost:5000",
            name: "victoria.jpg",
        };

        // Act
        const result = GetImageUrl(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should return undefined, when name is not provided.", () => {
        // Arrange
        const input = {
            base: "http://localhost:5000",
            name: "",
        };

        // Act
        const result = GetImageUrl(input);

        // Assert
        expect(result).toBe(undefined);
    });
});
