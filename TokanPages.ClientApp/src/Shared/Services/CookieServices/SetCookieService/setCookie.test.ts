/**
* @jest-environment jsdom
*/
import "../../../../setupTests";
import { SetCookie } from "..";

describe("verify cookies module functionality", () => 
{
    it("should return cookie string when 'days' and 'exact' value are defined.", () => 
    {
        // Arrange
        let date = new Date();
        const days = 3;

        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        const newExpiry = date.toUTCString();
 
        const expectedValue = `cookieConsent=granted; expires=${newExpiry}; path=/; SameSite=Strict;`;
        const expectedValueSecure = `cookieConsent=granted; expires=${newExpiry}; path=/; SameSite=Strict; Secure;`;

        const unsecureInput = 
        {
            cookieName: "cookieConsent", 
            value: "granted", 
            days: days,
            sameSite: "Strict",
            secure: false,
            exact: newExpiry
        }

        const secureInput = 
        {
            cookieName: "cookieConsent", 
            value: "granted", 
            days: days,
            sameSite: "Strict",
            secure: true,
            exact: newExpiry
        }

        // Act
        const unsecureResult = SetCookie(unsecureInput);
        const secureResult = SetCookie(secureInput);

        // Assert
        expect(unsecureResult).toBe(expectedValue);
        expect(secureResult).toBe(expectedValueSecure);
    });

    it("should return cookie string when only 'days' value is defined.", () => 
    {
        // Arrange
        let date = new Date();
        const days = 3;
 
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        const newExpiry = date.toUTCString();
 
        const expectedValue = `cookieConsent=granted; expires=${newExpiry}; path=/; SameSite=Strict;`;

        const input = 
        {
            cookieName: "cookieConsent", 
            value: "granted", 
            days: days,
            sameSite: "Strict",
            secure: false,
            exact: undefined
        } 

        // Act
        const result = SetCookie(input);

        // Assert
        expect(result).toBe(expectedValue);
    });

    it("should return cookie string as 'Secure' when SameSite is None regardless of secure property.", () => 
    {
        // Arrange
        let date = new Date();
        const days = 3;

        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        const newExpiry = date.toUTCString();
 
        const expectedValue = `cookieConsent=granted; expires=${newExpiry}; path=/; SameSite=None; Secure;`;

        const unsecureInput = 
        {
            cookieName: "cookieConsent", 
            value: "granted", 
            days: days,
            sameSite: "None",
            secure: false,
            exact: undefined
        } 

        const secureInput =
        {
            cookieName: "cookieConsent", 
            value: "granted", 
            days: days,
            sameSite: "None",
            secure: true,
            exact: undefined
        } 

        // Act
        const resultUnsecure = SetCookie(unsecureInput);
        const resultSecure = SetCookie(secureInput);

        // Assert
        expect(resultUnsecure).toBe(expectedValue);
        expect(resultSecure).toBe(expectedValue);
    });

    it("should return empty string when cookie days are set to zero.", () => 
    {
        // Arrange
        let date = new Date();
        const days = 0;

        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));

        const newExpiry = date.toUTCString();
        const expectedValue = "";

        const input = 
        {
            cookieName: "cookieConsent", 
            value: "granted", 
            days: days,
            sameSite: "Strict",
            secure: false,
            exact: newExpiry
        } 

        // Act
        const result = SetCookie(input);

        // Assert
        expect(result).toBe(expectedValue);
    });
});
 