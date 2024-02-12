import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { ProgressBarView } from "./progressBarView";

describe("test progress bar component", () => {
    it("should correctly render 'CenteredCircularLoader' component.", () => {
        const tree = shallow(<ProgressBarView />);
        expect(tree).toMatchSnapshot();
    });
});
