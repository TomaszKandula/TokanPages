import "../../../setupTests";
import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import { render } from "enzyme";
import { ContentDocumentState } from "../../../Store/States";
import { TextItemDto } from "../../../Api/Models";
import { DocumentView } from "./documentView";

describe("test component: documentView", () => {
    it("should render correctly '<DocumentView />' when policy content is loaded.", () => {
        const textItem: TextItemDto = {
            id: "5b30be71-0e68-4be4-bd29-7f40fe130414",
            type: "html",
            value: "<p>Policy text</p>",
            prop: "",
            text: "",
        };

        const document: ContentDocumentState = {
            contentPolicy: {
                isLoading: false,
                content: {
                    language: "eng",
                    items: [textItem],
                },
            },
        };

        const isLoading = document.contentPolicy?.isLoading ?? false;
        const items = document.contentPolicy?.content.items ?? [];
        const html = render(
            <Router>
                <DocumentView isLoading={isLoading} items={items} />
            </Router>
        );
        expect(html).toMatchSnapshot();
    });

    it("should render correctly '<DocumentView />' when terms content is loaded.", () => {
        const textItem: TextItemDto = {
            id: "5fcb46d8-cf3e-4ee4-91ab-c9285452de02",
            type: "html",
            value: "<p>Terms text</p>",
            prop: "",
            text: "",
        };

        const document: ContentDocumentState = {
            contentTerms: {
                isLoading: false,
                content: {
                    language: "eng",
                    items: [textItem],
                },
            },
        };

        const isLoading = document.contentTerms?.isLoading ?? false;
        const items = document.contentTerms?.content.items ?? [];
        const html = render(
            <Router>
                <DocumentView isLoading={isLoading} items={items} />
            </Router>
        );
        expect(html).toMatchSnapshot();
    });

    it("should render correctly '<DocumentView />' when story content is loaded.", () => {
        const textItem: TextItemDto = {
            id: "ca8643c8-45d6-4123-8c61-0fb1a6aa0711",
            type: "html",
            value: "<p>Story text</p>",
            prop: "",
            text: "",
        };

        const document: ContentDocumentState = {
            contentStory: {
                isLoading: false,
                content: {
                    language: "eng",
                    items: [textItem],
                },
            },
        };

        const isLoading = document.contentStory?.isLoading ?? false;
        const items = document.contentStory?.content.items ?? [];
        const html = render(
            <Router>
                <DocumentView isLoading={isLoading} items={items} />
            </Router>
        );
        expect(html).toMatchSnapshot();
    });

    it("should render correctly '<DocumentView />' when showcase content is loaded.", () => {
        const textItem: TextItemDto = {
            id: "08092d6c-9c3e-4bb5-8e51-e0b2388c3033",
            type: "html",
            value: "<p>Showcase text</p>",
            prop: "",
            text: "",
        };

        const document: ContentDocumentState = {
            contentShowcase: {
                isLoading: false,
                content: {
                    language: "eng",
                    items: [textItem],
                },
            },
        };

        const isLoading = document.contentShowcase?.isLoading ?? false;
        const items = document.contentShowcase?.content.items ?? [];
        const html = render(
            <Router>
                <DocumentView isLoading={isLoading} items={items} />
            </Router>
        );
        expect(html).toMatchSnapshot();
    });
});
