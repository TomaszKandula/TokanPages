import "../../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { ProgressBarView } from "./progressBarView";

describe("test progress bar component", () => {
    it("should correctly render 'CenteredCircularLoader' component.", () => {
        const html = render(<ProgressBarView />);
        expect(html).toMatchSnapshot();
    });
});
