import "../../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { SideMenuView } from "./sideMenuView";
import { Item } from "../../../../Shared/Components/RenderMenu/Models";

describe("test component: menuView", () => {
    it("should render correctly '<MenuView />' when content is loaded.", () => {
        const item: Item = {
            id: "8c678e9e-699e-41c7-a93a-587c6a4f41e7",
            type: "item",
            value: "Sign in",
            link: "/en/signin",
            icon: "VpnKey",
            enabled: true,
        };

        const html = render(
            <SideMenuView
                drawerState={{ open: false }}
                closeHandler={jest.fn()}
                isAnonymous={true}
                languageId="en"
                menu={{ image: "", items: [item] }}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
