/**
 * @jest-environment jsdom
 */
import "../../../../setupTests";
import { GetCookie, EraseCookie } from "..";

describe("verify cookies module functionality", () => {
    let date = new Date();
    const present = date.toUTCString();

    it("should return empty string on a given cookie value.", () => {
        // Arrange
        Object.defineProperty(window.document, "cookie", {
            writable: true,
            value: `cookieConsent=granted; expires=${present}; path=/; SameSite=Strict;`,
        });

        const expectation = "";

        // Act
        EraseCookie({ cookieName: "cookieConsent" });
        const result = GetCookie({ cookieName: "cookieConsent" });

        // Assert
        expect(result).toBe(expectation);
    });
});
