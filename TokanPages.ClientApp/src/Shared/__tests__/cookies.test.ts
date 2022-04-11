/**
 * @jest-environment jsdom
 */

import { SetCookie, GetCookie, EraseCookie } from "../cookies";

describe("Verify cookies module functionality.", () => 
{
    let date = new Date();
    const present = date.toUTCString();
    
    it("When SetCookie. Should return cookie string when 'days' and 'exact' value are defined.", () => 
    {
        let date = new Date();
        const days = 3;

        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        const newExpiry = date.toUTCString();

        const expectedValue = `cookieConsent=granted; expires=${newExpiry}; path=/; SameSite=Strict;`;
        const expectedValueSecure = `cookieConsent=granted; expires=${newExpiry}; path=/; SameSite=Strict; Secure;`;
    
        expect( 
            SetCookie(
            {
                cookieName: "cookieConsent", 
                value: "granted", 
                days: days,
                sameSite: "Strict",
                secure: false,
                exact: newExpiry
            } 
        )).toBe(expectedValue);

        expect( 
            SetCookie(
            {
                cookieName: "cookieConsent", 
                value: "granted", 
                days: days,
                sameSite: "Strict",
                secure: true,
                exact: newExpiry
            } 
        )).toBe(expectedValueSecure);
    });

    it("When SetCookie. Should return cookie string when only 'days' value is defined.", () => 
    {
        let date = new Date();
        const days = 3;

        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        const newExpiry = date.toUTCString();

        const expectedValue = `cookieConsent=granted; expires=${newExpiry}; path=/; SameSite=Strict;`;
    
        expect( 
            SetCookie(
            {
                cookieName: "cookieConsent", 
                value: "granted", 
                days: days,
                sameSite: "Strict",
                secure: false,
                exact: undefined
            } 
        )).toBe(expectedValue);
    });

    it("When SetCookie. Should return cookie string as 'Secure' when SameSite is None regardless of secure property.", () => 
    {
        let date = new Date();
        const days = 3;

        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        const newExpiry = date.toUTCString();

        const expectedValue = `cookieConsent=granted; expires=${newExpiry}; path=/; SameSite=None; Secure;`;
    
        expect( 
            SetCookie(
            {
                cookieName: "cookieConsent", 
                value: "granted", 
                days: days,
                sameSite: "None",
                secure: false,
                exact: undefined
            } 
        )).toBe(expectedValue);

        expect( 
            SetCookie(
            {
                cookieName: "cookieConsent", 
                value: "granted", 
                days: days,
                sameSite: "None",
                secure: true,
                exact: undefined
            } 
        )).toBe(expectedValue);
    });
    
    it("When SetCookie. Should return empty string when cookie days are set to zero.", () => 
    {
        let date = new Date();
        const days = 0;

        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        
        const newExpiry = date.toUTCString();
        const expectedValue = "";
    
        expect( 
            SetCookie(
            {
                cookieName: "cookieConsent", 
                value: "granted", 
                days: days,
                sameSite: "Strict",
                secure: false,
                exact: newExpiry
            } 
        )).toBe(expectedValue);
    });

    it("When GetCookie. Should return cookie value if such cookie exists.", () => 
    {
        Object.defineProperty(window.document, "cookie", 
        {
            writable: true,
            value: `cookieConsent=granted; expires=${present}; path=/; SameSite=Strict;`,
        });

        expect(GetCookie({ cookieName: "cookieConsent" })).toBe("granted");
    });

    it("When GetCookie. Should return empty string if cookie does not exists.", () => 
    {
        Object.defineProperty(window.document, "cookie", 
        {
            writable: true,
            value: `cookieConsent=granted; expires=${present}; path=/; SameSite=Strict;"`,
        });

        expect(GetCookie({ cookieName: "someCookie" })).toBe("");
    });

    it("When EraseCookie. Should return empty string on given cookie value.", () => 
    {
        Object.defineProperty(window.document, "cookie", 
        {
            writable: true,
            value: `cookieConsent=granted; expires=${present}; path=/; SameSite=Strict;`,
        });

        EraseCookie({ cookieName: "cookieConsent" });
        expect(GetCookie({ cookieName: "cookieConsent" })).toBe("");
    });
});
