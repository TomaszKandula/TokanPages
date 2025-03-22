import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { NavigationView } from "../../Navigation/View/navigationView";
import { Item } from "../../../../Shared/Components/RenderMenu/Models";
import { ApplicationLanguageState } from "../../../../Store/States";

describe("test component: featuresView", () => {
    it("should render correctly '<NavigationView />' when content is loaded.", () => {
        const items: Item = {
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
            <BrowserRouter>
                <NavigationView
                    isLoading={false}
                    drawerState={{ open: false }}
                    openHandler={jest.fn()}
                    closeHandler={jest.fn()}
                    infoHandler={jest.fn()}
                    isAnonymous={false}
                    avatarName=""
                    avatarSource=""
                    userAliasText=""
                    menu={{ image: "", items: [items] }}
                    logoImgName="logo.svg"
                    languages={languages}
                    languageId="en"
                    languageHandler={jest.fn()}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
