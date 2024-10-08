import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { FooterView } from "../View/footerView";
import { IconDto } from "../../../../Api/Models";

describe("test component: footerView", () => {
    it("should render correctly '<FooterView />' when content is loaded.", () => {
        const icons: IconDto = {
            name: "LinkedInIcon",
            href: "https://www.linkedin.com/",
        };

        const html = render(
            <BrowserRouter>
                <FooterView
                    terms={{ text: "Terms of use", href: "/terms" }}
                    policy={{ text: "Privacy policy", href: "/policy" }}
                    versionInfo="1.0"
                    hasVersionInfo={false}
                    backgroundColor="#FFFFFF"
                    copyright="© 2020 - 2023 Tomasz Kandula"
                    reserved="All rights reserved"
                    icons={[icons]}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
