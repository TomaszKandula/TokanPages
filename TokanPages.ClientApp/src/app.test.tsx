/**
* @jest-environment jsdom
*/
import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import { IGetContentManifestDto } from "./Api/Models";
import App from "./app";

it("renders without crashing", () => 
{
    const storeFake = (state: any) => (
    {
        default: () => 
        {  
            // Leave blank for tests
        },
        subscribe: () => 
        {
            // Leave blank for tests
        },
        dispatch: () => 
        {
            // Leave blank for tests
        },
        getState: () => ({ ...state })
    });
    
    const store = storeFake({ }) as any;
    const dto: IGetContentManifestDto = 
    {
        version: "",
        created: "",
        updated: "",
        languages: []
    }

    ReactDOM.render(
        <Provider store={store}>
            <MemoryRouter>
                <App manifest={dto} />
            </MemoryRouter>
        </Provider>, document.createElement("div")
    );
});
