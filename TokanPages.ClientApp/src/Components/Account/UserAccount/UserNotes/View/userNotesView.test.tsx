import "../../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { UserNotesView } from "./userNotesView";

describe("test account group component: userNotesView", () => {
    it("should render correctly '<UserNotesView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UserNotesView
                    isLoading={false}
                    hasProgress={false}
                    captionText={"User Notes"}
                    descriptionText={"You can save up to 100000 private notes..."}
                    listLabel={"Notes"}
                    noteLabel={"Message"}
                    onRowClick={jest.fn()}
                    clearButtonText={"Clear"}
                    clearButtonHandler={jest.fn()}
                    removeButtonText={"Remove"}
                    removeButtonHandler={jest.fn()}
                    saveButtonText={"Save"}
                    saveButtonHandler={jest.fn()}
                    messageForm={{ note: "" }}
                    messageHandler={jest.fn()}
                    messageMultiline={false}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });

    it("should render correctly '<UserNotesView />' when content is loading.", () => {
        const html = render(
            <BrowserRouter>
                <UserNotesView
                    isLoading={true}
                    hasProgress={false}
                    captionText={"User Notes"}
                    descriptionText={"You can save up to 100000 private notes..."}
                    listLabel={"Notes"}
                    noteLabel={"Message"}
                    onRowClick={jest.fn()}
                    clearButtonText={"Clear"}
                    clearButtonHandler={jest.fn()}
                    removeButtonText={"Remove"}
                    removeButtonHandler={jest.fn()}
                    saveButtonText={"Save"}
                    saveButtonHandler={jest.fn()}
                    messageForm={{ note: "" }}
                    messageHandler={jest.fn()}
                    messageMultiline={false}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });

    it("should render correctly '<UserNotesView />' when content is loaded and has progress.", () => {
        const html = render(
            <BrowserRouter>
                <UserNotesView
                    isLoading={false}
                    hasProgress={true}
                    captionText={"User Notes"}
                    descriptionText={"You can save up to 100000 private notes..."}
                    listLabel={"Notes"}
                    noteLabel={"Message"}
                    onRowClick={jest.fn()}
                    clearButtonText={"Clear"}
                    clearButtonHandler={jest.fn()}
                    removeButtonText={"Remove"}
                    removeButtonHandler={jest.fn()}
                    saveButtonText={"Save"}
                    saveButtonHandler={jest.fn()}
                    messageForm={{ note: "" }}
                    messageHandler={jest.fn()}
                    messageMultiline={false}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
