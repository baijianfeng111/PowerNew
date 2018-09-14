/*
Navicat MySQL Data Transfer

Source Server         : 192.168.1.82
Source Server Version : 50528
Source Host           : 192.168.1.82:3306
Source Database       : powerbjf

Target Server Type    : MYSQL
Target Server Version : 50528
File Encoding         : 65001

Date: 2018-03-22 10:10:19
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for bjf_account
-- ----------------------------
DROP TABLE IF EXISTS `bjf_account`;
CREATE TABLE `bjf_account` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL COMMENT '客户名',
  `mobile` varchar(255) DEFAULT NULL COMMENT '客户手机号',
  `domainname` varchar(255) DEFAULT NULL COMMENT '客户域名',
  `domainshortname` varchar(255) DEFAULT NULL COMMENT '域名简写',
  `dbserver` varchar(255) DEFAULT NULL COMMENT '数据库地址',
  `dbname` varchar(255) DEFAULT NULL COMMENT '数据库名字',
  `dbuserid` varchar(255) DEFAULT NULL COMMENT '数据库账号',
  `dbpassword` varchar(255) DEFAULT NULL COMMENT '数据库密码',
  `comment` varchar(255) DEFAULT NULL COMMENT '备注',
  `state` int(11) DEFAULT NULL COMMENT '状态',
  `createid` int(11) NOT NULL,
  `createtime` datetime NOT NULL,
  `updateid` int(11) NOT NULL,
  `updatetime` datetime NOT NULL,
  `isdelete` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='账户管理表';

-- ----------------------------
-- Records of bjf_account
-- ----------------------------
INSERT INTO `bjf_account` VALUES ('1', '小米', '9=A9A@>88:;', 'bjf.login.com', 'xiaomi', '192.168.1.82', 'xiaomi', 'root', 'zwux9:;H', '首次测试', '0', '0', '2018-03-21 17:59:28', '0', '2018-03-21 17:59:28', '\0');

-- ----------------------------
-- Table structure for bjf_block
-- ----------------------------
DROP TABLE IF EXISTS `bjf_block`;
CREATE TABLE `bjf_block` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `blockname` varchar(255) NOT NULL,
  `isuse` int(11) NOT NULL DEFAULT '0',
  `sortcode` int(11) NOT NULL,
  `isdelete` bit(1) NOT NULL DEFAULT b'0',
  `deletetime` datetime DEFAULT NULL,
  `createid` int(11) NOT NULL,
  `createtime` datetime NOT NULL,
  `updateid` int(11) NOT NULL,
  `updatetime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bjf_block
-- ----------------------------
INSERT INTO `bjf_block` VALUES ('1', '基础管理', '2078', '1', '\0', null, '1', '2017-10-18 14:24:04', '1', '2017-09-05 17:32:28');
INSERT INTO `bjf_block` VALUES ('2', '业务管理', '0', '2', '\0', null, '1', '2017-09-05 17:36:52', '1', '2017-09-05 17:36:55');
INSERT INTO `bjf_block` VALUES ('3', 'OA管理', '0', '3', '\0', null, '1', '2017-10-18 14:19:33', '1', '2017-09-05 17:37:21');
INSERT INTO `bjf_block` VALUES ('4', '系统管理', '0', '4', '\0', null, '1', '2017-10-18 14:21:17', '1', '2017-10-18 14:17:25');
INSERT INTO `bjf_block` VALUES ('5', 'GOOd', '0', '5', '', null, '1', '2017-10-18 14:19:40', '1', '2017-10-18 14:19:44');
INSERT INTO `bjf_block` VALUES ('6', 'asdffsd00', '1', '77', '', null, '1', '2017-10-19 16:56:47', '1', '2017-11-02 09:42:58');
INSERT INTO `bjf_block` VALUES ('7', '新1212', '0', '6', '\0', null, '0', '2017-11-02 17:08:39', '2', '2017-11-03 11:55:16');
INSERT INTO `bjf_block` VALUES ('8', '1232123123', '0', '855555', '\0', null, '1', '2017-11-13 14:17:01', '2', '2017-12-05 15:25:44');
INSERT INTO `bjf_block` VALUES ('9', '测试', '0', '12', '\0', null, '2', '2017-12-22 11:27:09', '2', '2017-12-22 11:27:09');
INSERT INTO `bjf_block` VALUES ('10', '小模块', '0', '12', '\0', null, '0', '2017-12-25 15:48:38', '0', '2017-12-25 15:48:38');
INSERT INTO `bjf_block` VALUES ('11', '123123', '0', '122', '\0', null, '0', '2017-12-25 15:57:49', '0', '2017-12-25 15:57:49');
INSERT INTO `bjf_block` VALUES ('12', 'AAA', '0', '0', '\0', null, '3', '2018-01-23 16:21:30', '3', '2018-01-23 16:21:30');

-- ----------------------------
-- Table structure for bjf_loginlog
-- ----------------------------
DROP TABLE IF EXISTS `bjf_loginlog`;
CREATE TABLE `bjf_loginlog` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ip` varchar(255) DEFAULT NULL,
  `userid` int(11) NOT NULL,
  `logintime` datetime NOT NULL,
  `createtime` datetime NOT NULL,
  `updatetime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bjf_loginlog
-- ----------------------------
INSERT INTO `bjf_loginlog` VALUES ('1', '192.168.1.25', '3', '2018-01-23 16:14:19', '2018-01-23 16:14:19', '2018-01-23 16:14:19');
INSERT INTO `bjf_loginlog` VALUES ('2', '192.168.1.25', '3', '2018-01-26 11:04:11', '2018-01-26 11:04:11', '2018-01-26 11:04:11');
INSERT INTO `bjf_loginlog` VALUES ('3', '192.168.1.25', '3', '2018-01-30 17:48:01', '2018-01-30 17:48:01', '2018-01-30 17:48:01');
INSERT INTO `bjf_loginlog` VALUES ('4', '192.168.1.25', '3', '2018-03-05 11:15:11', '2018-03-05 11:15:11', '2018-03-05 11:15:11');
INSERT INTO `bjf_loginlog` VALUES ('5', '192.168.1.25', '3', '2018-03-05 14:16:03', '2018-03-05 14:16:03', '2018-03-05 14:16:03');
INSERT INTO `bjf_loginlog` VALUES ('6', '192.168.1.25', '3', '2018-03-12 13:30:47', '2018-03-12 13:30:47', '2018-03-12 13:30:47');
INSERT INTO `bjf_loginlog` VALUES ('7', '192.168.1.25', '3', '2018-03-12 14:21:53', '2018-03-12 14:21:53', '2018-03-12 14:21:53');
INSERT INTO `bjf_loginlog` VALUES ('8', '192.168.1.25', '3', '2018-03-12 14:33:07', '2018-03-12 14:33:07', '2018-03-12 14:33:07');

-- ----------------------------
-- Table structure for bjf_menu
-- ----------------------------
DROP TABLE IF EXISTS `bjf_menu`;
CREATE TABLE `bjf_menu` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `menuname` varchar(255) NOT NULL,
  `menucode` varchar(255) NOT NULL,
  `blockid` int(11) DEFAULT NULL,
  `menuhref` varchar(255) DEFAULT NULL,
  `isgroup` bit(1) NOT NULL COMMENT '0表示菜单组，1表示子菜单',
  `parentid` int(11) DEFAULT '0' COMMENT '父类id',
  `isuse` bit(1) NOT NULL DEFAULT b'0',
  `sortcode` int(11) NOT NULL,
  `isdelete` bit(1) NOT NULL DEFAULT b'0',
  `deletetime` datetime DEFAULT NULL,
  `createid` int(11) NOT NULL,
  `createtime` datetime NOT NULL,
  `updateid` int(11) NOT NULL,
  `updatetime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bjf_menu
-- ----------------------------
INSERT INTO `bjf_menu` VALUES ('1', '基础菜单', 'JC001', '2', 'http://192.168.1.25:8013/Menu/Index', '\0', '0', '\0', '0', '\0', null, '1', '2017-11-06 15:35:19', '1', '2017-11-06 15:35:19');
INSERT INTO `bjf_menu` VALUES ('2', '用户管理', 'yh001', '4', '/Menu/Create', '\0', '0', '\0', '0', '\0', null, '1', '2017-11-08 16:24:48', '1', '2017-11-08 16:24:48');
INSERT INTO `bjf_menu` VALUES ('3', '系统设置', 'xt001', '4', 'Menu/Create', '', '0', '\0', '0', '\0', null, '1', '2017-11-08 16:28:11', '1', '2017-11-08 16:28:11');
INSERT INTO `bjf_menu` VALUES ('4', '用户密码管理', 'mm001', '4', 'Menu/mima', '\0', '2', '\0', '0', '\0', null, '1', '2017-11-08 16:28:55', '1', '2017-11-08 16:28:55');
INSERT INTO `bjf_menu` VALUES ('5', '设置', 'sz001', '4', 'Menu/Create', '', '3', '\0', '0', '\0', null, '1', '2017-11-08 16:29:23', '1', '2017-11-08 16:29:23');
INSERT INTO `bjf_menu` VALUES ('6', '基本', 'jb001', '4', 'Menu/Create', '', '3', '\0', '0', '\0', null, '1', '2017-11-08 16:29:52', '1', '2017-11-08 16:29:52');
INSERT INTO `bjf_menu` VALUES ('7', '测试', 'cs002', '7', 'http://www.login.com/Menu/Create', '', '0', '\0', '0', '\0', null, '1', '2017-11-08 16:55:53', '1', '2017-11-08 16:55:53');

-- ----------------------------
-- Table structure for bjf_role
-- ----------------------------
DROP TABLE IF EXISTS `bjf_role`;
CREATE TABLE `bjf_role` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `openid` varchar(255) NOT NULL,
  `rolename` varchar(255) NOT NULL,
  `rolecode` varchar(255) NOT NULL,
  `isuse` int(11) NOT NULL DEFAULT '0' COMMENT '0启用，1作废',
  `createid` int(11) NOT NULL,
  `createtime` datetime NOT NULL,
  `isdelete` bit(1) NOT NULL DEFAULT b'0',
  `deletetime` datetime DEFAULT NULL,
  `updateid` int(11) NOT NULL,
  `updatetime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bjf_role
-- ----------------------------
INSERT INTO `bjf_role` VALUES ('1', 'b6c45f22bc1e48a79c7131278fb7ad01', '销售', 'XS001', '0', '1', '2017-09-06 10:15:09', '\0', null, '1', '2017-09-06 10:15:09');
INSERT INTO `bjf_role` VALUES ('2', 'be46819c334f4235b66feb76330858a3', '店员', 'DY001', '0', '1', '2017-09-06 10:15:29', '\0', null, '1', '2017-09-06 10:15:29');
INSERT INTO `bjf_role` VALUES ('3', '9a1140ce33e0407485f628a204d43d0f', '财务人员312', 'CW001', '1', '1', '2017-09-06 10:15:44', '\0', null, '1', '2017-10-18 14:04:00');

-- ----------------------------
-- Table structure for bjf_roleformenu
-- ----------------------------
DROP TABLE IF EXISTS `bjf_roleformenu`;
CREATE TABLE `bjf_roleformenu` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `roleid` int(11) NOT NULL,
  `menuid` int(11) NOT NULL,
  `createtime` datetime NOT NULL,
  `createid` int(11) NOT NULL,
  `updateid` int(11) NOT NULL,
  `updatetime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bjf_roleformenu
-- ----------------------------

-- ----------------------------
-- Table structure for bjf_roleforuser
-- ----------------------------
DROP TABLE IF EXISTS `bjf_roleforuser`;
CREATE TABLE `bjf_roleforuser` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `roleid` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `isdelete` bit(11) NOT NULL DEFAULT b'0',
  `deletetime` datetime NOT NULL,
  `createtime` datetime NOT NULL,
  `createid` int(11) NOT NULL,
  `updateid` int(11) NOT NULL,
  `updatetime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bjf_roleforuser
-- ----------------------------

-- ----------------------------
-- Table structure for bjf_user
-- ----------------------------
DROP TABLE IF EXISTS `bjf_user`;
CREATE TABLE `bjf_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `openid` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `loginname` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `email` varchar(255) DEFAULT NULL,
  `idcard` varchar(255) DEFAULT NULL,
  `mobile` varchar(255) NOT NULL,
  `isadmin` int(11) NOT NULL DEFAULT '1' COMMENT '0超管，1非超管',
  `comment` varchar(255) DEFAULT NULL,
  `isdelete` bit(1) NOT NULL DEFAULT b'0',
  `deletetime` datetime DEFAULT NULL,
  `createid` int(11) NOT NULL,
  `createtime` datetime NOT NULL,
  `updateid` int(11) NOT NULL,
  `updatetime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bjf_user
-- ----------------------------
INSERT INTO `bjf_user` VALUES ('3', '4da59ebc784347ba9e530330cd0b9839', 'admin', 'admin', '9:;<=>', '806078508@qq.com', null, 'NrOufDC8JamI8Jog4WHI6CcPwYe0fn7V8Pj2WBnsmXn9CuZYoXmuFch0eU96GMbSdig86XhMliQaC+l9GeeqDar3TMy+ueu4rgyGEQwxTsq5boP5RxfON8yCq91W0S03onbLqVw5tG6IMiAXcvVfuaJxareiXGxpIgODDC6VIno=', '0', '123456', '\0', null, '1', '2018-01-23 16:14:02', '1', '2018-03-21 17:34:30');
