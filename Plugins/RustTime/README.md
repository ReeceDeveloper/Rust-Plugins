# RustTime

The RustTime plugin provides server owners the ability to configure a permanent time as well
as the length of the night-time hours of their Rust server.

## Default (Plugin) Configuration

Below is the default plugin configuration settings should you need to reset your configuration.

```json
{
  "(1). Enable the RustTime plugin?": true,
  "(2). Enable a static (permanent) time?": false,
  "(3). If enabled, set the static time.": 12,
  "(4). Speed up Rust's night phase?": false,
  "(5). If enabled, set how long night should last (minutes).": 15,
  "(6). Announce when the time becomes faster?": true,
  "(7). Announce when the time returns to normal?": true
}
```

## Default (Language) Configuration

Below is the default language configuration settings should you need to reset your language file.

```json
{
  "TimeSpeedNormalEvent": "Time will now resume its normal speed.",
  "TimeSpeedFastEvent": "Time will now speed up during night."
}
```

To use colour codes use `[#HEXCODE]Text[/#]` to do such; for example, `[#123ABC]Text[/#]`.

## MIT License

Copyright © 2021 ReeceDeveloper

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
documentation files (the “Software”), to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
DEALINGS IN THE SOFTWARE.