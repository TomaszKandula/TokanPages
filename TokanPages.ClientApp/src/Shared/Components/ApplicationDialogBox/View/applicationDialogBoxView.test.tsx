import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { IconType } from "../../../enums";
import { ApplicationDialogBoxView } from "../View/applicationDialogBoxView";

//TODO: use render
describe("test view component for application diaog box", () => {
    it("should render correctly view component with passed props.", () => {
        const html = shallow(
            <ApplicationDialogBoxView
                state={true}
                icon={IconType.info}
                title="Test title"
                message="Test message"
                closeHandler={jest.fn()}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
