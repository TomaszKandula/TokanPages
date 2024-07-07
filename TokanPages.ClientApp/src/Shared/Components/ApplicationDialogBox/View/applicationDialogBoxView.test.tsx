import "../../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { IconType } from "../../../enums";
import { ApplicationDialogBoxView } from "../View/applicationDialogBoxView";

describe("test view component for application diaog box", () => {
    it("should render correctly view component with passed props.", () => {
        const html = render(
            <ApplicationDialogBoxView
                state={true}
                icon={IconType.info}
                title={"Test title"}
                message={"Test message"}
                closeHandler={jest.fn()}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
