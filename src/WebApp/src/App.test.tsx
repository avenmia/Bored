import * as React from "react";
import { render } from "@testing-library/react";
import App from "./App";

describe("<App>", () => {
  it("renders the app", () => {
    const { getByText } = render(<App />);
  });
});
