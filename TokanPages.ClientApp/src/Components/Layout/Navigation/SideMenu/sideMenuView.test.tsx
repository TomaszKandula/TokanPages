import "../../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { ItemDto } from "../../../../Api/Models";
import { SideMenuView } from "./sideMenuView";

describe("test component: menuView", () => {
    it("should render correctly '<MenuView />' when content is loaded.", () => {
        const item: ItemDto = {
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
