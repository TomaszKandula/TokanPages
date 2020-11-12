import { IsEmpty } from "../helpers";

test("Check if string is empty or not.", () => 
{
    expect( IsEmpty("string") ).toBe(false);
});
