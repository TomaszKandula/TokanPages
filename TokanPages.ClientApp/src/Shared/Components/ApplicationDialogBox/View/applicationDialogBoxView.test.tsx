import "../../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { IconType } from "../../../enums";
import { ApplicationDialogBoxView } from "../View/applicationDialogBoxView";

describe("test view component for application diaog box", () => {
    it("should render correctly view component with passed props.", () => {
        const html = render(
            <ApplicationDialogBoxView
                state={true}
                icon={IconType.info}
                title="Test title"
                message={["Test item 1", "Test item 2"]}
                closeHandler={jest.fn()}
                disablePortal={true}
                hideBackdrop={true}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
