import "../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { CookiesView } from "./cookiesView";

describe("test component: cookiesView", () => {
    it("should render correctly '<CookiesView />' when content is loaded.", () => {
        const html = render(
            <CookiesView
                isLoading={false}
                modalClose={false}
                shouldShow={false}
                caption="Cookie Policy"
                text="We use cookies to personalise content..."
                onClickEvent={jest.fn()}
                buttonText="Accept cookies"
            />
        );

        expect(html).toMatchSnapshot();
    });
});
