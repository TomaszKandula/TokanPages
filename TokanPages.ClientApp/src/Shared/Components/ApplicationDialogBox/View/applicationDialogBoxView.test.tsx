import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { IconType } from "../../../enums";
import { ApplicationDialogBoxView } from "../View/applicationDialogBoxView";

describe("test view component for application diaog box", () => 
{
    it("should render correctly view component with passed props.", () => 
    {
        const tree = shallow(<ApplicationDialogBoxView
            state={true}
            icon={IconType.info}
            title={"Test title"}
            message={"Test message"}
            closeHandler={jest.fn()}        
        />);

        expect(tree).toMatchSnapshot();
    });
});
