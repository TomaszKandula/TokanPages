import "../../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { ActivateAccountView } from "./activateAccountView";

describe("test account group component: activateAccountView", () => {
    it("should render correctly '<ActivateAccountView />' when content is loaded.", () => {
        const html = render(
            <ActivateAccountView
                isLoading={false}
                caption={"Account Activation"}
                text1={"Your account has been successfully activated!"}
                text2={"You can now sign in."}
                progress={false}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
