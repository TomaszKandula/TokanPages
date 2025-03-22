import "../../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { CollapsibleView } from "./collapsibleView";

describe("test collapsible component: CollapsibleView", () => {
    it("should render correctly '<CollapsibleView />' when content is not expanded.", () => {
        const ref = React.createRef<HTMLDivElement>();
        const html = render(
            <CollapsibleView isOpen={false} height={20} clickHandler={jest.fn()} reference={ref}>
                <div>
                    <p>Test line 1</p>
                    <p>Test line 2</p>
                    <p>Test line 3</p>
                </div>
            </CollapsibleView>
        );

        expect(html).toMatchSnapshot();
    });

    it("should render correctly '<CollapsibleView />' when content is expanded.", () => {
        const ref = React.createRef<HTMLDivElement>();
        const html = render(
            <CollapsibleView isOpen={true} height={20} clickHandler={jest.fn()} reference={ref}>
                <div>
                    <p>Test line 1</p>
                    <p>Test line 2</p>
                    <p>Test line 3</p>
                </div>
            </CollapsibleView>
        );

        expect(html).toMatchSnapshot();
    });
});
