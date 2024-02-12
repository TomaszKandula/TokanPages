/**
 * @jest-environment jsdom
 */
import "../../../../setupTests";
import { GetCookie } from "..";

describe("verify cookies module functionality", () => {
    let date = new Date();
    const present = date.toUTCString();

    it("should return cookie value if such cookie exists.", () => {
        // Arrange
        const expectation = "granted";
        const cookieName = { cookieName: "cookieConsent" };

        Object.defineProperty(window.document, "cookie", {
            writable: true,
            value: `cookieConsent=${expectation}; expires=${present}; path=/; SameSite=Strict;`,
        });

        // Act
        const result = GetCookie(cookieName);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should return empty string if cookie does not exists.", () => {
        // Arrange
        const expectation = "";
        const cookieName = { cookieName: "someCookie" };

        Object.defineProperty(window.document, "cookie", {
            writable: true,
            value: `cookieConsent=granted; expires=${present}; path=/; SameSite=Strict;"`,
        });

        // Act
        const result = GetCookie(cookieName);

        // Assert
        expect(result).toBe(expectation);
    });
});
