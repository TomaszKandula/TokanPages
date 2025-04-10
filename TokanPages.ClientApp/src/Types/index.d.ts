import { Unhead } from "unhead/client";
import { ResolvableHead } from "unhead/types";

export {};
declare global {
    interface Window {
        __UNHEAD__: Unhead<ResolvableHead>;
    }
}
