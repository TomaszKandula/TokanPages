import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { ProgressBarView } from "./progressBarView";

describe("Test progress bar component.", () => 
{
    it("Should correctly render 'CenteredCircularLoader' component.", () => 
    {
        const tree = shallow(<ProgressBarView />);
        expect(tree).toMatchSnapshot();
    });
});
