import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { CookiesView } from "./cookiesView";

describe("test component: cookiesView", () => {
    it("should render correctly '<CookiesView />' when content is loaded.", () => {
        const tree = shallow(
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

        expect(tree).toMatchSnapshot();
    });
});
