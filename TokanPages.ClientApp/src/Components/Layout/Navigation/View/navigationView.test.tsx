import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { NavigationView } from "../../Navigation/View/navigationView";
import { ItemDto } from "../../../../Api/Models";
import { ApplicationLanguageState } from "../../../../Store/States";

describe("test component: NavigationView", () => {
    it("should render correctly '<NavigationView />' when content is loaded.", () => {
        const items: ItemDto = {
            id: "79a6c65d-08b8-479b-9507-97feb05e30c2",
            type: "item",
            value: "Home",
            link: "/",
            icon: "Home",
            enabled: true,
        };

        const languages: ApplicationLanguageState = {
            id: "en",
            languages: [
                {
                    id: "en",
                    iso: "en-GB",
                    name: "English",
                    isDefault: true,
                },
                {
                    id: "pl",
                    iso: "pl-PL",
                    name: "Polski",
                    isDefault: false,
                },
            ],
            warnings: [],
            pages: [
                {
                    language: "en",
                    pages: [
                        {
                            title: "software developer",
                            page: "MainPage",
                            description: "software development",
                        },
                    ],
                },
            ],
            meta: [
                {
                    language: "en",
                    facebook: {
                        title: "",
                        description: "",
                        imageAlt: "",
                    },
                    twitter: {
                        title: "",
                        description: "",
                    },
                },
            ],
            errorBoundary: [
                {
                    language: "en",
                    title: "Critical Error",
                    subtitle: "Something went wrong...",
                    text: "Contact the site's administrator or support for assistance.",
                    linkHref: "mailto:admin@tomkandula.com",
                    linkText: "IT support",
                    footer: "tomkandula.com",
                },
            ],
        };

        const html = render(
                <NavigationView
                    isLoading={false}
                    isAnonymous={false}
                    isMenuOpen={false}
                    media={{ isDesktop: true, isMobile: false, isTablet: false, width: 430, height: 932 }}
                    triggerSideMenu={jest.fn()}
                    infoHandler={jest.fn()}
                    avatarName=""
                    avatarSource=""
                    aliasName=""
                    menu={{ image: "", items: [items] }}
                    logo="logo.svg"
                    languages={languages}
                    languageId="en"
                    languagePickHandler={jest.fn()}
                    languageMenuHandler={jest.fn()}
                    isLanguageMenuOpen={false}
                    backPathHandler={jest.fn()}
                />
            , { wrapper: BrowserRouter }
        );

        expect(html).toMatchSnapshot();
    });
});
