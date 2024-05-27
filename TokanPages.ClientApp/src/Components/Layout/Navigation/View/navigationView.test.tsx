import "../../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { NavigationView } from "../../Navigation/View/navigationView";
import { Item } from "../../../../Shared/Components/ListRender/Models";
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
            id: "eng",
            languages: [
                {
                    id: "eng",
                    name: "English",
                    isDefault: true,
                },
                {
                    id: "pol",
                    name: "Polski",
                    isDefault: false,
                },
            ],
        };

        const html = render(
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
                languageId="eng"
                languageHandler={jest.fn()}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
