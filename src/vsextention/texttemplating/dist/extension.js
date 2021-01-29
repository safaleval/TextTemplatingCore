module.exports =
/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ([
/* 0 */
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.deactivate = exports.activate = void 0;
// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
const vscode = __webpack_require__(1);
// this method is called when your extension is activated
// your extension is activated the very first time the command is executed
function activate(context) {
    let NEXT_TERM_ID = 1;
    // Use the console to output diagnostic information (console.log) and errors (console.error)
    // This line of code will only be executed once when your extension is activated
    console.log('Congratulations, your extension "texttemplating" is now active!');
    // The command has been defined in the package.json file
    // Now provide the implementation of the command with registerCommand
    // The commandId parameter must match the command field in package.json
    let disposable = vscode.commands.registerCommand('tt.helloWorlds', () => {
        // The code you place here will be executed every time your command is executed
        // Display a message box to the user
        vscode.window.showInformationMessage('Hello World from texttemplating!');
    });
    context.subscriptions.push(vscode.commands.registerCommand('tt.helloWorld', (uri) => {
        const terminal = vscode.window.createTerminal(`TT #${NEXT_TERM_ID++}`);
        terminal.sendText(`dotnet tt '${uri.fsPath}'`);
        terminal.show();
    }));
    context.subscriptions.push(vscode.commands.registerCommand('terminalTest.show', () => {
        if (ensureTerminalExists()) {
            selectTerminal().then(terminal => {
                if (terminal) {
                    terminal.show();
                }
            });
        }
    }));
    //https://github.com/microsoft/vscode-extension-samples/blob/master/terminal-sample/src/extension.ts
    context.subscriptions.push(disposable);
}
exports.activate = activate;
// this method is called when your extension is deactivated
function deactivate() { }
exports.deactivate = deactivate;
function colorText(text) {
    let output = '';
    let colorIndex = 1;
    for (let i = 0; i < text.length; i++) {
        const char = text.charAt(i);
        if (char === ' ' || char === '\r' || char === '\n') {
            output += char;
        }
        else {
            output += `\x1b[3${colorIndex++}m${text.charAt(i)}\x1b[0m`;
            if (colorIndex > 6) {
                colorIndex = 1;
            }
        }
    }
    return output;
}
function selectTerminal() {
    const terminals = vscode.window.terminals;
    const items = terminals.map(t => {
        return {
            label: `name: ${t.name}`,
            terminal: t
        };
    });
    return vscode.window.showQuickPick(items).then(item => {
        return item ? item.terminal : undefined;
    });
}
function ensureTerminalExists() {
    if (vscode.window.terminals.length === 0) {
        vscode.window.showErrorMessage('No active terminals');
        return false;
    }
    return true;
}


/***/ }),
/* 1 */
/***/ ((module) => {

module.exports = require("vscode");;

/***/ })
/******/ 	]);
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		if(__webpack_module_cache__[moduleId]) {
/******/ 			return __webpack_module_cache__[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId](module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	// module exports must be returned from runtime so entry inlining is disabled
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })()
;
//# sourceMappingURL=extension.js.map